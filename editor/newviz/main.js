$(function () {

  var storageKey = 'dialogic-editor-code';
  var lastSelection = "";
  var myTextarea = $("#main")[0];

  var editor = CodeMirror.fromTextArea(myTextarea, {
    lineNumbers: true,
    styleSelectedText: true
  });

  var chatData = {
    chats: ["CHAT NewChat"],
    nodes: [{ "id": 0, "label": "NewChat" }],
    edges: []
  }

  // network
  var nodes = new vis.DataSet(chatData.nodes);
  var edges = new vis.DataSet();

  var opts = {
    nodes: {
      shape: 'box'
    },
    manipulation: false
  };

  var network = new vis.Network(document.getElementById('network'), {
    nodes: nodes,
    edges: edges
  }, opts);

  // Variables
  var isEditMode = false;
  var currentTextId = 1;

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
    if (!isResizing)
      return;
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

  // ******** State Editor ************//
  // loadFromStorage();
  toggleNetworkView("split");
  updateContent("Enter your code here");

  // var lastEdit = editor.getValue();

  editor.on("beforeSelectionChange", function (cm, change) {
    lastSelection = editor.getSelection();
  });

  editor.on("cursorActivity", function (cm) {
    toggleValidation();
  });

  // editor.on("beforeChange", function(cm, change) {
  //   lastEdit = editor.getValue();
  // });

  editor.on("change", function (cm, change) {
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
    var toSave = chatData.chats.join("\n")
    saveFile(toSave, "chats");
  });

  $('#importFilePicker').on('change', handleFileLoader);

  $("#loadChats").click(onLoadClicked);

  $("#clearLoad").click(function () {
    $("#urlPath").val('');
    $("#importFilePicker").val('');
  });

  $("#VRcheckbox").click(function () {
    localStorage.setItem("useValidators", $("#VRcheckbox").prop('checked'));
    toggleValidation();
  });

  $("#closeDialog").click(closeURLDialog);

  // ******** End Click Handlers ************//

  onLoad();
  updateNetworkViewer();

  // ******** Start Functions ************//
  function onLoad() {
    toggleValidation();
    // showSelection(); // This must be placed before toggleExe
    toggleExe();
  }

  function updateEditor(response, type) {

    $("#result-container").show();

    switch (type) {

    case "validate":


      if (response.status == "OK") {
        $("#result").attr("class", "success");
      } else {
        $("#result").attr("class", "error");
        //if something is selected, update line number
        if (editor.somethingSelected()) {
          var startIdx = editor.getCursor(true);
          var errorLine = parseInt(/Line\s+([\d]+)/g.exec(response.data)[1]);
          errorLine = startIdx.line + errorLine;
          response.data = "Line " + response.data.replace(/Line\s+([\d]+)/g, errorLine + "");
        }
        highlightErrorLine(errorLine);
      }
      $("#result").html(jsonUnescape(response.data));
      break;

    case "execute":
      $("#executeResult").text(jsonUnescape(response.data));
      if (response.status == "OK") {
        $("#executeResult").attr("class", "success");
      } else {
        $("#executeResult").attr("class", "error");
      }
      break;

    default:
      throw Error('bad type: ' + type);
    }

    onLoad();
  }

  function closeNetWorkView() {
    toggleNetworkView(false);
    $(".showNetwork").show();
  }

  function jsonEscape(str) {
    return str.replace(/\n/g, "\\\\n").replace(/\r/g, "\\\\r").replace(/\t/g, "\\\\t");
  }

  function jsonUnescape(str) {
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
    var input = editor.getValue(),
      selection = editor.getSelection();

    if ((input.length > 0 && input != 'Enter your code here') || selection != lastSelection) {
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

  function toggleExe() {
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
      // formData.push(
      // {
      //   name: "selection",
      //   value: execute ? $("#result").text() :
      //       processSelectedText(content, startIdx.line, endIdx.line)
      // });
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

  function onLoadClicked() {
    // console.log($("#loadPathDiv").serialize())
    sendRequest($("#loadPathDiv").serialize());
  }

  function saveFile(data, name) {
    var dataStr = "data:text/plain;charset=utf-8," + data;
    var dlAnchorElem = document.getElementById('downloadAnchorElem');
    dlAnchorElem.setAttribute("href", dataStr);
    dlAnchorElem.setAttribute("download", name + ".gs");
    dlAnchorElem.click();
  }

  function handleFileLoader(evt) {
    var files = evt.target.files;
    var reader = new FileReader();
    var fileList = [];
    var allData;

    // clear all the old data
    nodes.clear();
    edges.clear();
    var newData = chatData = {
      chats: [],
      nodes: [],
      edges: []
    };

    Array.prototype.forEach.call(files, function (file) {
      var reader = new FileReader();

      reader.onload = function (e) {
        var dialogData;
        try {
          var chats = e.target.result.split(/(?=CHAT)/g);
          for (var i = 0; i < chats.length; i++) {
            if (chats[i].indexOf("CHAT") != 0) continue;
            newData.chats.push(chats[i]);
            label = /CHAT\s+([a-zA-Z_\d]+)/g.exec(chats[i].split("/n")[0])[0];
            var node = {};
            node["id"] = newData.chats.length - 1;
            node["label"] = label;
            newData.nodes.push(node);
            //TODO: parse edges
          }
          // console.log(newData)
          chatsOnLoadHandler(newData);
        } catch (e) {
          console.log(e);
          return;
        }
      }

      reader.readAsText(file);
    }); // Finish all the files

    $("#loadURLDialog").hide();
  }

  function chatsOnLoadHandler(data) {
    if (isValidData(data)) {
      //add to current chatData
      chatData.chats.concat(data.chats);
      chatData.nodes.concat(data.nodes);
      chatData.edges.concat(data.edges);
      nodes.update(data.nodes);
      edges.update(data.edges);
    }

    updateNetworkViewer();

    var nodeId = nodes.get()[0].id;
    editChat(nodeId);
    network.focus(nodeId + "");
    network.selectNodes([nodeId])
  }

  function updateNetworkViewer() {
    network.setData({
      nodes: nodes,
      edges: edges
    });
  }

  function isValidData(data) {
    // TODO: better validation?
    if (data.chats != undefined && data.edges != undefined && data.nodes != undefined) return true;
    else return false;
  }

  function loadFromStorage() {
    if (localStorage) {
      var content = localStorage.getItem(storageKey);
      if (content != undefined && content != 'Enter your code here') {
        updateContent(chatData.chats[nodeId]);
      }

      var editorHeight = localStorage.getItem("editorHeight");
      if (editorHeight != null) {
        editor.setSize(null, editorHeight);
      }

      var useValidators = localStorage.getItem("useValidators");
      if (useValidators != null) {
        $("#VRcheckbox").prop('checked', useValidators === "true" ? true : false);
      }
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
    //tmp
    //console.log('payload', data, data.type);
    //var  = data.type;
    ///data = JSON.stringify(data);

    var server = "http://localhost:8082/dialogic/server/";
    $.ajax({
      type: 'POST',
      data: JSON.stringify(data),
      //data: "{'type':'validate','code':'Hello'}",
      url: server,
      crossDomain: true,
      processData: false,
      dataType: 'json',
      contentType: "application/json",
      success: function (result) {
        updateEditor(result, data.type);
      },
      error: function (xhr, status, err) {

        console.log("ERROR", status, err, xhr);
      }
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

  function editChat(nodeId, callback) {
    // console.log("editChat: ", nodeId, nodes.get(nodeId), chats[nodeId]);
    // load the editor with id=data.id, name=data.label
    toggleNetworkView("split");
    updateContent(chatData.chats[nodeId]);
    currentTextId = nodeId
  }

  /**** Network UI ***/
  network.on('doubleClick', function (event) {
    if (event.nodes.length > 0) {
      // On node: open editor
      editChat(event.nodes[0]);

    } else {
      var newId = addNode(event);
      network.disableEditMode();
      editNode(event, newId);
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

  function updateChatLabelInScript(originalScript, label) {
    return originalScript.replace(/CHAT\s+([a-zA-Z_\d]+)/g, label);
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
