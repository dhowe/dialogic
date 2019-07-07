$(function () {

  var storageKey = 'dialogic-editor-code';
  var myTextarea = $("#main")[0];
  var lastSelection = "";
  var defaultLabel = "Untitled";
  var defaultText = "CHAT " + defaultLabel + "\nEnter your code here";

  var editor = CodeMirror.fromTextArea(myTextarea, {
    lineNumbers: true,
    styleActiveLine: true,
    styleSelectedText: true
  });

  var opts = {
    nodes: {
      shape: 'box'
    },
    layout: { improvedLayout: true },
    autoResize: true,
    manipulation: false,
    physics:{
      enabled:true,
      solver:'forceAtlas2Based',
      minVelocity:1
    }
  };

  var edges = new vis.DataSet();
  var nodes = new vis.DataSet([{ id: labelToId(defaultLabel), label: defaultLabel }]);
  var network = new vis.Network(document.getElementById('network'), {
    nodes: nodes,
    edges: edges
  }, opts);

  //console.log(network);

  // Variables
  var isEditMode = false;
  var currentTextId = 1;
  var state = {
    pos: undefined,
    scale: undefined
  };

  // ******** General UI ************//

  // Resizing
  var isResizing = false,
    lastDownX = 0;

  var container = $('#container'),
    left = $('#network'),
    right = $('#editor'),
    handle = $('#drag');

  handle.on('mousedown', function (e) {
    isResizing = true;
    lastDownX = e.clientX;
  });

  $(document).on('mousemove', function (e) {
    if (!isResizing) return;
    var offsetRight = container.width() - (e.clientX - container.offset().left);
    left.css('right', offsetRight);
    var newLeft = (window.innerWidth - offsetRight) / window.innerWidth * 100 + "%";
    var newRight = offsetRight / window.innerWidth * 100 + "%";
    left.css('width', newLeft);
    right.css('width', newRight);

  }).on('mouseup', function (e) {
    // stop resizing
    isResizing = false;
  });

  // var lastEdit = editor.getValue();

  editor.on("beforeSelectionChange", function (cm, change) {
    lastSelection = editor.getSelection();
  });

  // Not trigger when on load

  editor.on("cursorActivity", function (cm) {

    var selection = editor.getSelection();
    if(selection != lastSelection) toggleValidation();

    //TODO: don't set editor state on load'
    //remember both activeLine & scrollPosition
    localStorage.setItem("editorActiveLine", editor.getCursor().line);
    localStorage.setItem("editorScrollPosition",JSON.stringify(editor.getScrollInfo()));
  });

  editor.on("scroll", function (cm) {
    // TODO: only set storage after scroll?
    localStorage.setItem("editorScrollPosition",JSON.stringify(editor.getScrollInfo()));
  });

  // editor.on("beforeChange", function(cm, change) {
  //   lastEdit = editor.getValue();
  // });

  editor.on("changes", function (cm, changes) {
    // update the textarea
    var content = editor.getValue();
    $("#main").val(content);
    /* save current change to cookie
    if (content != "Enter your script here")
    {
      $.cookie("guppyScript", content);
    }*/
    // save current change to storage
    if (content != "Enter your code here") {
      localStorage.setItem(storageKey, content);
    }
    toggleValidation();
  });
  // ******** End Editor ************//

  // ******** Click Handlers **********//
  $(".editor-close").click(function () {
    toggleNetworkView(true);
  });

  $(".vis-close").click(function () {
    // TODO: this is not working
    closeNetWorkView();
  });

  $(".showNetwork").click(function () {
    toggleNetworkView("split");
    $(".showNetwork").hide();
  });

  // setup button handlers
  $("#clear").click(function () {
    updateContent("");
    editor.clearHistory();
    localStorage.removeItem(storageKey);
    nodes.clear();
    edges.clear();
    $("#result-container pre").html("");
  });

  $("#execute").click(function (event) {
    onButtonClicked(event, true);
  });

  $("#validate").click(function (event) {
    onButtonClicked(event, false);
  });

  // handle dialog show/hide
  $("#showDialog").click(function () {
    $("#loadURLDialog").show();
  });

  $("#saveChats").click(function () {
      $('.downloadInterface').show();
  });
  $(".downloadInterface button").click(function () {
    $('.downloadInterface').hide();
    saveFile(editor.getValue(), "chats");
  });

  $('#txt').on('keypress', function() {
    if ($('#txt').width() < 120) this.style.width = ((this.value.length + 1) * 8) + 'px';
  })

  $('#importFilePicker').on('change', handleFileLoader);

  //$("#loadChats").click(onLoadClicked);

  $("#clearLoad").click(function () {
    $("#urlPath").val('');
    $("#importFilePicker").val('');
  });

  $("#VRcheckbox").click(function () {
    localStorage.setItem("useValidators", $("#VRcheckbox").prop('checked'));
    toggleValidation();
  });

  $("#closeDialog").click(closeURLDialog);

  // loadFromStorage();
  toggleNetworkView("split");
  updateButtons();

  // // zoom in on the single node
  // setTimeout(bestFit, 1000);

  // ============================ Functions ===================================
  function updateButtons() {
    toggleValidation();
    // showSelection(); // This must be placed before toggleExecute
    toggleExecute();
  }

  function toNetworkData(chats) {
    var nodes = [],
      edges = [];

    var labelToIdLookup = {};

    // NEXT: WORKING HERE *******
    for (var i = 0; i < chats.length; i++) {
      labelToIdLookup[chats[i].Name] = labelToId(chats[i].Name);
    }

    for (var i = 0; i < chats.length; i++) {
      var id = labelToIdLookup[chats[i].Name];
      nodes.push({ id: id, label: chats[i].Name });
      for (var j = 0; j < chats[i].Labels.length; j++) {
        edges.push({
          from: id,
          to: labelToIdLookup[chats[i].Labels[j]],
          arrows: 'to'
        });
      }
    }
    return {
      nodes: nodes,
      edges: edges
    };
  }

  function getCreatedNodes(nodes) {
    var nodesToAdd = [];
    var graphNodeIndices = network.body.nodeIndices;
    var graphNodeLookup = network.body.nodes;

    FOR: for (var i = 0; i < nodes.length; i++) {
      var newLabel = nodes[i].label;
      for (var j = 0; j < graphNodeIndices.length; j++) {
        var graphNode = graphNodeLookup[graphNodeIndices[j]];
        if (newLabel === graphNode.options.label) continue FOR;
      }
      nodesToAdd.push(nodes[i]);
    }
    return nodesToAdd;
  }

  function getDeletedNodeIds(nodes) {
    var nodesToDel = [];
    var graphNodeIndices = network.body.nodeIndices;
    var graphNodeLookup = network.body.nodes;
    FOR: for (var i = 0; i < graphNodeIndices.length; i++) {
      var graphNode = graphNodeLookup[graphNodeIndices[i]];
      var graphLabel = graphNode.options.label;
      for (var j = 0; j < nodes.length; j++) {
        if (nodes[j].label === graphLabel) continue FOR;
      }
      nodesToDel.push({ id: graphNode.id, node: graphNode });
    }
    return nodesToDel;
  }

  // NEXT: create unique labels for nodes

  function labelToId(str) {
    var id = 0,
      i, chr, len;
    if (str.length === 0) return id;
    // generate unique ID https://gist.github.com/gordonbrander/2230317
    id = Math.random().toString(36).substr(2, 9);
    return id;
  }

  function bestFit() {
    //resume pan and zoom
    if (state.pos != undefined) {
      // console.log(state.pos, state.scale)
       network.moveTo({
         position: state.pos,
         scale: state.scale
       })
       state.pos = undefined;
       state.scale = undefined;
       return;
     }

    network.moveTo({ scale: 1 });
    network.stopSimulation();

    var bigBB = { top: Infinity, left: Infinity, right: -Infinity, bottom: -Infinity }
    nodes.getIds().forEach(function (i) {
      var bb = network.getBoundingBox(i);
      if (bb.top < bigBB.top) bigBB.top = bb.top;
      if (bb.left < bigBB.left) bigBB.left = bb.left;
      if (bb.right > bigBB.right) bigBB.right = bb.right;
      if (bb.bottom > bigBB.bottom) bigBB.bottom = bb.bottom;
    })

    var canvasWidth = network.canvas.body.container.clientWidth;
    var canvasHeight = network.canvas.body.container.clientHeight;

    var scaleX = canvasWidth / (bigBB.right - bigBB.left);
    var scaleY = canvasHeight / (bigBB.bottom - bigBB.top);
    var scale = scaleX;
    if (scale * (bigBB.bottom - bigBB.top) > canvasWidth) scale = scaleY;

    if (scale > 1) scale = 0.9 * scale;

    network.moveTo({
      scale: Math.min(scale, 2),
      position: {
        x: (bigBB.right + bigBB.left) / 2,
        y: (bigBB.bottom + bigBB.top) / 2
      }
    })
  }

  function updateGraph(response, isSelection) { // on validate only

    // Parse the returned data into nodes-and-edges
    var chats = tryParse(response.data);
    var data = toNetworkData(chats);
    // Remove all nodes not in returned set (if not a selection)
    if (!isSelection) {
      var dIds = getDeletedNodeIds(data.nodes);
      nodes.remove(dIds);
    }

    // Add all nodes with new labels
    var newNodes = getCreatedNodes(data.nodes);

    // remove node with timeStamp if it is caused by partial selection
    if (isSelection && chats.length > 1) {
      for (var i = 0; i < newNodes.length; i++) {
        var isTimeStamp = /C\d{9}/g.exec(newNodes[i].label);
        if (isTimeStamp != null) newNodes.splice(i, 1);
      }
    }

    nodes.add(newNodes);

    // OPT: handle nodes with label-changes?

    // Now update all the edges
    edges.add(data.edges); // TODO: handle edges on selection

    // Now fit to the window
    network.fit();
  }

  function nextNodeId() {
    var ids = nodes.getIds();
    console.log(ids);
    return ids[ids.length - 1] + 1;
  }

  // convert an array of Chats to text for editor pane
  function updateContentFromChats(chats) { // not used
    var txt = '';
    var chats = tryParse(response.data);
    for (var i = 0; i < chats.length; i++) {
      txt += chats[i].Data + "\n\n";
    }
    updateContent(txt);
  }

  function onDataChange(response, data) {

    //console.log("RAW", response.status, response.data);
    switch (data.type) {

    case "validate":

      $("#executeResult").hide();
      $("#result-container").show();

      if (response.status == "OK") {

        $("#result").attr("class", "success");

        // strip leading blank lines
        var code = data.code.replace(/^(\r?\n)+/, '');
        $("#result").html(code);

        if (data.fileload) updateContent(data.code);

        updateGraph(response, data.selection === true);

      } else {
        $("#result").html(response.data.split('\n\n')[1] + " (line " + response.lineNo + ")");
        $("#result").attr("class", "error");
        highlightErrorLine(response.lineNo);
      }
      break;

      // case "validate":
      //     $("#result").attr("class", "error");
      //     //if something is selected, update line number
      //     if (editor.somethingSelected()) {
      //       var startIdx = editor.getCursor(true);
      //       var errorLine = parseInt(/Line\s+([\d]+)/g.exec(response.data)[1]);
      //       errorLine = startIdx.line + errorLine;
      //       response.data = "Line " + response.data.replace(/Line\s+([\d]+)/g, errorLine + "");
      //     }
      //     highlightErrorLine(errorLine);
      //   }
      //   $("#result").html(jsonUnescape(response.data));

    case "execute":
      $("#result-container").show();
      $("#executeResult").show();
      $("#executeResult").text(jsonUnescape(response.data));

      // TODO: add warning for label-issues (loop or fail)
      $("#executeResult").attr("class", (response.status == "OK" ? "success" : "error"));
      break;

    default:
      throw Error('bad type: ' + type);
    }

    updateButtons();
  }

  function tryParse(str) {
    try {
      str = str.replace(/\\/g, "\\\\").replace(/\n/g, "\\n"); // yuck
      return JSON.parse(str);
    } catch (e) {
      console.error("FAILED TO PARSE: '" + str + "'");
      for (var i = 0; i < str.length; i++) {
        console.error(i, str[i]);
      }
      throw e;
    }
  }

  function closeNetWorkView() {
    toggleNetworkView(false);
    $(".showNetwork").show();
  }

  function jsonEscape(str) { // ?
    return str.replace(/\n/g, "\\\\n").replace(/\r/g, "\\\\r").replace(/\t/g, "\\\\t");
  }

  function jsonUnescape(str) { // ?
    return str.replace(/\\\\n/g, "\n").replace(/\\\\r/g, "\r").replace(/\\\\t/g, "\t");
  }

  function highlightErrorLine(errorLine) {
    if (errorLine > 0) {
      var idx = errorLine - 1;
      editor.addLineClass(idx, "background", 'line-error');
    }
  }

  function showSelection() {
    if (!$("#selection").text().includes("STARTINDEX")) {
      var idx = $("#selection").text().split(";");
      idx[0] = idx[0].split(",");
      idx[1] = idx[1].split(",");
      editor.setSelection({
        line: idx[0][0],
        ch: idx[0][1]
      }, {
        line: idx[1][0],
        ch: idx[1][1]
      });
    }
  }

  function toggleValidation() {
    var input = editor.getValue()

    if (input.length > 0 ) {
      $("#validate").prop("disabled", false);
      $("#execute").prop("disabled", true);
    }

    if (input.length == 0) {
      $("#clear").prop("disabled", true);
      $("#validate").prop("disabled", true);
    } else {
      $("#clear").prop("disabled", false);
    }
  }

  function toggleExecute() {
    var resText = $("#result").text();
    // only show execute if validation is successfull
    if (resText.length && $("#result").attr('class') == "success") {
      $("#execute").prop("disabled", false);
      $("#validate").prop("disabled", true);
    }
  }

  function closeURLDialog() {
    $("#loadURLDialog").hide();
    $("#urlPath").html("Paste your url");
  }

  function onButtonClicked(event, execute) {
    state.pos = state.pos == undefined ? network.getViewPosition() : state.pos;
    state.scale = state.scale == undefined ? network.getScale() : state.scale;

    event.preventDefault();

    var formData = $("#form").serializeArray(); //json

    formData.push({
      name: 'type',
      value: execute ? 'execute' : 'validate'
    });

    if (editor.somethingSelected()) {
      formData[0].value = editor.getSelection();
      // var content = processSelectedText(formData[0].value, startIdx.line, endIdx.line);
      // formData.push(
      // {
      //   name: "selectionStart",
      //   value: startIdx.line + "," + startIdx.ch
      // });
      //
      // formData.push(
      // {
      //   name: "selectionEnd",
      //   value: endIdx.line + "," + endIdx.ch
      // });
      //
      formData.push({
        name: 'selection',
        value: true
      });
    }
    sendRequest(formToObj(formData));
  }

  function formToObj(form) {
    var obj = {};
    for (let i = 0; i < form.length; i++) {
      obj[form[i].name] = form[i].value;
    }
    return obj;
  }

  function saveFile(data, name) {
    var blob = new Blob([data], { type: 'text/plain' });
    var anchor = document.getElementById('downloadAnchorElem');
    anchor.setAttribute("href", (window.webkitURL || window.URL).createObjectURL(blob));
    anchor.setAttribute("download", name + ".gs");
    anchor.click();
  }

  function readFiles(files, cb) {
    var text = '';
    function readFile(idx) {
      var reader = new FileReader();
      reader.onload = function (e) {
        //console.log(idx + ').onload: ', e.target.result);
        text += e.target.result;
        if (idx == files.length - 1) cb(text);
        else readFile(idx + 1);
      }
      reader.readAsText(files[idx]);
    }
    readFile(0);
  }

  function handleFileLoader(evt) {
    readFiles(evt.target.files, function (ctext) {
      sendRequest({ type: 'validate', code: ctext, fileload: true });
      $("#loadURLDialog").hide();
    });
  }

  function loadFromStorage() {
    if (localStorage) {

      // Settings
      var useValidators = localStorage.getItem("useValidators");
      if (useValidators != null) {
        $("#VRcheckbox").prop('checked', useValidators === "true" ? true : false);
      }

      // Content
      var content = localStorage.getItem(storageKey);
      if (content != undefined && content != 'Enter your code here') {
          updateContent(content);
      } else {
          updateContent(defaultText);
          return; // only continue if content != undefined
      }

      // TODO: Fix editor & network state resume

      // Editor states
      // var editorHeight = localStorage.getItem("editorHeight");
      // if (editorHeight != null) {
      //   editor.setSize(null, editorHeight);
      // }
      //
      // var editorActiveLine = localStorage.getItem("editorActiveLine");
      // if (editorActiveLine != null) {
      //   console.log(editorActiveLine);
      //   editor.setCursor({ line: editorActiveLine});
      // }

      // var editorScrollPosition = JSON.parse(localStorage.getItem("editorScrollPosition"));
      // if (editorScrollPosition != null) {
      //   var left = editorScrollPosition.left;
      //   var top = editorScrollPosition.top;
      //   var right = editorScrollPosition.width;
      //   var bottom = editorScrollPosition.height;
      //   editor.scrollIntoView({left, top, right, bottom});
      // }

      // Network states

      // var networkPosition = localStorage.getItem("networkPosition");
      // if (networkPosition != null) {
      //   state.pos = JSON.parse(networkPosition);
      // }
      //
      // var networkScale = localStorage.getItem("networkScale");
      // if (networkScale != null) {
      //   state.scale = parseInt(networkScale) != 0 ? parseInt(networkScale) : 1;
      // }
      //
      // $("#validate").click();

    }
  }

  /*function loadFromCookie()
  {
    var content = $.cookie("guppyScript");
    if (content != undefined && content != 'Enter your script here')
    {
      editor.setValue(content);
      $("#main").val(content);
    }
  }*/

  function sendRequest(data) {

    //console.log('sendRequest', data, data.type);

    //var server = "http://localhost:8082/dialogic-server/";
    var server = "http://"+window.location.hostname+":8082/dialogic-server/";
    console.log('SERVER',server);
    $.ajax({
      type: 'POST',
      data: JSON.stringify(data),
      url: server,
      crossDomain: true,
      processData: false,
      dataType: 'json',
      contentType: "application/json",
      success: function (result) { onDataChange(result, data) },
      error: function (xhr, status, err) {
        console.error("ERROR", status, err, xhr.responseText, JSON.stringify(data));
      }
      //only update network when validatting all the chats
      //if (data.type == "validate" && data.selection != true) updateNetwork(result);
    });
  }

  function processSelectedText(code, startIdx, endIdx) {
    if (code) {

      // we need to maintain correct line numbers for error-reporting,
      // so if we have a partial selection, we comment out any lines
      // NOT included in the highlighted portion

      var lines = code.split("\n");

      for (var i = 0; i < lines.length; i++) {
        if (i < startIdx) lines[i] = "//" + lines[i];
        if (endIdx < i) lines[i] = "//" + lines[i];
      }
      return lines.join("\n");

    }
  }

  // Verticle resize
  // Based on: https://jsfiddle.net/mindplay/rs2L2vtb/2/
  let $handle = document.querySelector(".handle");
  let $container = document.querySelector(".CodeMirror");

  function height_of($el) {
    return parseInt(window.getComputedStyle($el).height.replace(/px$/, ""));
  }

  const MIN_HEIGHT = 200;

  var start_x;
  var start_y;
  var start_h;

  function on_drag(e) {
    var height = Math.max(MIN_HEIGHT, (start_h + e.y - start_y)) + "px";
    localStorage.setItem("editorHeight", height);
    editor.setSize(null, height);
  }

  function on_release(e) {
    document.body.removeEventListener("mousemove", on_drag);
    window.removeEventListener("mouseup", on_release);
  }

  $handle.addEventListener("mousedown", function (e) {
    start_x = e.x;
    start_y = e.y;
    start_h = height_of($container);

    document.body.addEventListener("mousemove", on_drag);
    window.addEventListener("mouseup", on_release);
  });

  function toggleNetworkView(val) {
    if (typeof val != 'string') {
      $("#editor").toggle(!val);
      $("#network").toggle(val);
      $(".showNetwork").toggle(!val);
      $("#editor, #network").width("100%");
      !val && setTimeout(function () {
        $("#editor").focus();
        $('#editor').trigger('click');
      }, 1000);
    } else if (val == "split") {
      $("#editor").toggle(true);
      $("#network").toggle(true);
      $("#editor").width("49%");
      $("#network").width("49%");
    }
  }

  function updateContent(content) {
    editor.setValue(content);
    $("#main").val(content);
  }

  function jumpToLine(i) {
    var t = editor.charCoords({line: i, ch: 0}, "local").top;
    editor.scrollTo(null, t - 5);
  }

  function editChat(nodeId, callback) {
    // console.log("editChat: ", nodeId, nodes.get(nodeId), chats[nodeId]);
    // load the editor with id=data.id, name=data.label
    toggleNetworkView("split");
    // On node: scroll to position
    currentTextId = nodeId;
    var label = network.body.data.nodes._data[currentTextId].label;
    var lines = editor.getValue().split("\n");
    for (var i = 0; i < lines.length; i++) {
      var parsedLabel = /CHAT\s+([a-zA-Z_\d]+)/g.exec(lines[i]);
      if (parsedLabel && label == parsedLabel[1]) {
        editor.setCursor({ line: i, ch: 0 })
        jumpToLine(i);
      }
    }
  }

  /**** Network UI ***/
  network.on('doubleClick', function (event) {
    if (event.nodes.length > 0) {
      editChat(event.nodes[0]);
    } else {

      // Onhold
      // var newId = addNode(event);
      // network.disableEditMode();
      // editNode(event, newId);
    }
  });

  // Right click / Ctrl click
  network.on('oncontext', function (e) {
    e.event.preventDefault();
    if (e.nodes.length > 0) {
      updatePopUpPosition(e, $('.popupMenu')[0])
      $('.popupMenu').show();
    }
  });

  network.on('click', function (e) {

    var clickOutsideMenu = function () {
      var networkDiv = $('.vis-network')[0];
      var popup = $('.popupMenu');
      var left = popup.position().left + networkDiv.offsetLeft;
      var top = popup.position().top + networkDiv.offsetTop;
      var right = left + popup.width();
      var bottom = top + popup.height();
      return e.event.center.x < left || e.event.center.x > right || e.event.center.y < top || e.event.center.y > bottom;
    }

    if ($('.popupMenu').is(":visible") && clickOutsideMenu()) {
      $('.popupMenu').hide();
    }

  });

  // network on pan/zoom
  network.on('zoom', function (e) {
    localStorage.setItem("networkScale", network.getScale());
  });

  network.on('dragEnd', function (e) {
    localStorage.setItem("networkPosition", JSON.stringify(network.getViewPosition()));
  });

  $('#deleteNode').on('click', function (e) {
    network.deleteSelected(event.nodes);
    $('.popupMenu').hide();
  });

  $('#chatLabel').keyup(function () {
    nodes.update({ "id": currentTextId, "label": $("#chatLabel").text() })
  });

  function updatePopUpPosition(e, popup) {
    var networkDiv = $('.vis-network')[0];
    mouseX = e.event.x ? e.event.x : e.event.center.x;
    mouseY = e.event.y ? e.event.y : e.event.center.y;
    popup.style.left = mouseX - networkDiv.offsetLeft + 'px';
    popup.style.top = mouseY - networkDiv.offsetTop - 50 + 'px';
  }

  function addNode(event) {
    var label = 'C(' + Date.now() + ")";
    var id = chatData.chats.length;

    nodes.add([{
      id: id,
      label: label,
      x: event.pointer.canvas.x,
      y: event.pointer.canvas.y,
    }]);

    // initialize chats
    chatData.chats.push("");
    chatData.nodes.push({
      id: id,
      label: label
    });
    network.selectNodes([id]);
    return id;
  }

  function saveNodeData(id, event) {

    if (isEditMode) {
      var label = $('#node-label')[0].value;
      nodes.update({ "id": id, "label": label });
      if (event.keyCode == 13) {
        if (isValidLabel(label)) {
          //update chatData.chats chatData.nodes,
          chatData.nodes[id]["label"] = label;
          chatData.chats[id] = "CHAT " + label;
        }
        $('#node-popUp').hide();
        isEditMode = false;
      }
    }
  }

  function isValidLabel(label) {
    if (label == "") {
      alert("Chat label can't be empty.");
      return false
    }
    // check all the chat labels
    for (var i = 0; i < chatData.nodes.length; i++) {
      if (label == chatData.nodes[i].label) {
        alert("Duplicate chat label, please use another chat label.");
        return false
      }
    }
    return true;
  }

  function getChatLabelFromId(id) {
    console.log(chatData.nodes)
    for (var i = 0; i < chatData.nodes.length; i++) {
      if (id == chatData.nodes[i].id) {
        return chatData.nodes[i].label;
      }
    }
  }

  function editNode(event, id) {
    isEditMode = true;
    document.getElementById('node-label').value = getChatLabelFromId(id);
    // $(document).keypress(function(e) {
    document.onkeypress = saveNodeData.bind(this, id);
    // document.getElementById('node-cancelButton').onclick = cancelAction.bind(this, callback);
    updatePopUpPosition(event, $('#node-popUp')[0]);
    $('#node-popUp').show();

  }

});
