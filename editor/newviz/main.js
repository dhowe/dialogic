var storageKey = 'dialogic-editor-code';
var lastSelection = "";
var myTextarea = $("#main")[0];
var editor = CodeMirror.fromTextArea(myTextarea,
{
  lineNumbers: true,
  styleSelectedText: true
});

  $(function ()
  {
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
        left.css('width', window.innerWidth - offsetRight);
        right.css('width', offsetRight);
    }).on('mouseup', function (e) {
        // stop resizing
        isResizing = false;
    });

    // ******** State Editor ************//
    loadFromStorage();
    // var lastEdit = editor.getValue();

    editor.on("beforeSelectionChange", function (cm, change)
    {
      lastSelection = editor.getSelection();
    });

    editor.on("cursorActivity", function (cm)
    {
      toggleValidation();
    });

    // editor.on("beforeChange", function(cm, change) {
    //   lastEdit = editor.getValue();
    // });

    editor.on("change", function (cm, change)
    {
      // update the textarea
      var content = editor.getValue();
      $("#main").val(content);

      /* save current change to cookie
      if (content != "Enter your script here")
      {
        $.cookie("guppyScript", content);
      }*/

      // save current change to storage
      if (content != "Enter your script here")
      {
        localStorage.setItem(storageKey, content);
      }

      toggleValidation();
    });
    // ******** End Editor ************//

    // ******** Click Handlers **********//
    $(".editor-close").click(function ()
    {
      toggleNetworkView(true);
    });
    $(".vis-close").click(function ()
    {
      toggleNetworkView(false);
    });
    $(".showNetwork").click(function ()
    {
      toggleNetworkView("split");
    });

    // setup button handlers
    $("#clear").click(function ()
    {
      updateContent("");
      editor.clearHistory();
      localStorage.removeItem(storageKey);
    });

    $("#execute").click(function (event)
    {
      onButtonClicked(event, true);
    });

    $("#validate").click(function (event)
    {
      onButtonClicked(event, false);
    });

    // handle dialog show/hide
    $("#showDialog").click(function ()
    {
      $("#loadURLDialog").show();
    });

    $("#loadURL").click(onLoadURLClicked);

    $("#clearURL").click(function ()
    {
      $("#urlPath").val('');
    });

    $("#VRcheckbox").click(function ()
    {
      localStorage.setItem("useValidators", $("#VRcheckbox").prop('checked'));
      toggleValidation();
    });

    $("#closeDialog").click(closeURLDialog);

    // ******** End Click Handlers ************//

    onLoad();

    // ******** Start Functions ************//
    function onLoad()
    {
      toggleValidation();
      // showSelection(); // This must be placed before toggleExe
      toggleExe();
      // highlightErrorLine();
    }

    function updateEditor(data, type) {
      var response = JSON.parse(jsonEscape(data));
      $("#result-container").show();

      switch (type) {
        case "validate":
        $("#result").text(inverseJsonEscape(response.data));
        if (response.status == "OK") {
            $("#result").attr("class","success");
        } else {
          $("#result").attr("class","error");
        }
        break;
        case "execute":
        $("#executeResult").text(inverseJsonEscape(response.data));
        if (response.status == "OK") {
            $("#executeResult").attr("class","success");
        } else {
          $("#executeResult").attr("class","error");
        }
        break;
        default:
      }
      onLoad();

    }

    function jsonEscape(str)  {
      return str.replace(/\n/g, "\\\\n").replace(/\r/g, "\\\\r").replace(/\t/g, "\\\\t");
    }

    function inverseJsonEscape(str) {
      return str.replace(/\\\\n/g, "\n").replace(/\\\\r/g, "\r").replace(/\\\\t/g, "\t");
    }


    function highlightErrorLine()
    {
      if (!$("#errorLine").text().includes("ERRORLINE"))
      {
        var idx = $("#errorLine").text() - 1;
        editor.addLineClass(idx, "background", 'line-error');
      }
    }

    function showSelection()
    {
      if (!$("#selection").text().includes("STARTINDEX"))
      {
        var idx = $("#selection").text().split(";");
        idx[0] = idx[0].split(",");
        idx[1] = idx[1].split(",");
        editor.setSelection(
        {
          line: idx[0][0],
          ch: idx[0][1]
        },
        {
          line: idx[1][0],
          ch: idx[1][1]
        });
      }
    }

    function toggleValidation()
    {
      var input = editor.getValue(),
        selection = editor.getSelection();

      if ((input.length > 0 && input != 'Enter your script here') || selection != lastSelection)
      {
        $("#validate").prop("disabled", false);
        $("#execute").prop("disabled", true);
      }

      if (input.length == 0)
      {
        $("#clear").prop("disabled", true);
        $("#validate").prop("disabled", true);
      }
      else
      {
        $("#clear").prop("disabled", false);
      }
    }

    function toggleExe()
    {
      var resText = $("#result").text();
      // only show execute if validation is successfull
      if (resText.length && $("#result").attr('class') == "success")
      {
        $("#execute").prop("disabled", false);
        $("#validate").prop("disabled", true);
      }
    }

    function closeURLDialog()
    {
      $("#loadURLDialog").hide();
      $("#urlPath").html("Paste your url");
    }

    function onButtonClicked(event, execute)
    {
      event.preventDefault();

      var formData = $("#form").serializeArray();
      formData.push(
      {
        name: 'type',
        value: execute ? 'execute' : 'validate'
      });

      if (editor.somethingSelected())
      {
        var content = editor.getValue();
        var startIdx = editor.getCursor(true);
        var endIdx = editor.getCursor(false);
        formData.push(
        {
          name: "selectionStart",
          value: startIdx.line + "," + startIdx.ch
        });

        formData.push(
        {
          name: "selectionEnd",
          value: endIdx.line + "," + endIdx.ch
        });

        formData.push(
        {
          name: "selection",
          value: execute ? $("#result").text() :
              processSelectedText(content, startIdx.line, endIdx.line)
        });

      }
      sendRequest(formData, execute ? 'execute' : 'validate');
    }

    function onLoadURLClicked()
    {
      sendRequest($("#loadUrlPathDiv").serialize());
    }

    function loadFromStorage()
    {
      if (localStorage)
      {
        var content = localStorage.getItem(storageKey);
        if (content != undefined && content != 'Enter your script here')
        {
          updateContent(content)
        }

        var editorHeight = localStorage.getItem("editorHeight");
        if (editorHeight != null)
        {
          editor.setSize(null, editorHeight);
        }

        var useValidators = localStorage.getItem("useValidators");
        if (useValidators != null)
        {
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

    function sendRequest(data, type)
    {
      //tmp
      var server = "http://localhost:8082/dialogic/editor/";
      $.ajax(
      {
        type: "POST",
        data: data,
        url: server,
        crossDomain: true,
        contentType: 'application/x-www-form-urlencoded',
        success: function (data) {
          updateEditor(data, type)
        },
        error: function (xhr, status) {
        }
      });

    }

    function processSelectedText(code, startIdx, endIdx)
    {
      if (code)
      {

        // we need to maintain correct line numbers for error-reporting,
        // so if we have a partial selection, we comment out any lines
        // NOT included in the highlighted portion

        var lines = code.split("\n");

        for (var i = 0; i < lines.length; i++)
        {
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

    function height_of($el)
    {
      return parseInt(window.getComputedStyle($el).height.replace(/px$/, ""));
    }

    const MIN_HEIGHT = 200;

    var start_x;
    var start_y;
    var start_h;

    function on_drag(e)
    {
      var height = Math.max(MIN_HEIGHT, (start_h + e.y - start_y)) + "px";
      localStorage.setItem("editorHeight", height);
      editor.setSize(null, height);
    }

    function on_release(e)
    {
      document.body.removeEventListener("mousemove", on_drag);
      window.removeEventListener("mouseup", on_release);
    }

    $handle.addEventListener("mousedown", function (e)
    {
      start_x = e.x;
      start_y = e.y;
      start_h = height_of($container);

      document.body.addEventListener("mousemove", on_drag);
      window.addEventListener("mouseup", on_release);
    });

  });

  function toggleNetworkView(val)
  {
    if (typeof val != 'string'){
      $("#editor").toggle(!val);
      $("#network").toggle(val);
      $(".showNetwork").toggle(!val);
      $("#editor, #network").width("100%");
      !val && setTimeout(function() {
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

  function updateContent(content){
      editor.setValue(content);
      $("#main").val(content);
  }

  function editChat(data, callback) {

    console.log("editChat: ", data, chats[data.id]);
    // load the editor with id=data.id, name=data.label
    toggleNetworkView("split");
    updateContent(chats[data.id]);
  }

  var chats = {
    "1": "CHAT start {preload=true}\nSET $emotion = simple | lost\nSET $place = Purgatory\nSET $neg = Nah | No | Nyet\nSET $verb = play | start | begin",
    "2": "CHAT GScriptTest {type=a,stage=b}\nSAY Welcome to my $emotion world\nNVM 1.1\nWAIT 3\nDO #Twirl\nWAIT {ForAnimation=true}\nSAY Thanks for visiting $place {speed=fast,style=whisper}\nGO #Prompt",
    "3": "CHAT RePrompt {type=a,stage=b}\nDO #SadSpin\nASK (Really|Awww), don't you want to play a game?\n  OPT sure #Game\n  OPT $neg #RePrompt",
    "4": "CHAT Prompt {notPlayed=true,type=a,stage=b}\nASK Do you want to $verb a game? {timeout=4,speed=fast}\n  OPT Sure #Game\n  OPT Nope #RePrompt",
    "5": "CHAT Game {type=a,stage=b,last=true}\nDO #HappyFlip {axis=y}\nSAY Great, let's play! {speed=slow,style=loud}\nSAY Bye! {speed=fast}",
    "6": "CHAT OnTapEvent {noStart=true,resumeAfter=true}\nDO #TapResponse\nSAY Ok, I see you!\nSAY Wait, is that (cat | dog | artichoke).articlize()?",
    "7": "CHAT MyWorld {noStart=true,chatMode=grammar}\nSET start = My world is a $adj, $adj place.\nSET adj = creepy | lonely | dark | forgotten | crepuscular\nSAY $start",
  };
  var nodes = new vis.DataSet([
    { id: 1, label: 'start' },
    { id: 2, label: 'GScriptTest' },
    { id: 3, label: 'RePrompt' },
    { id: 4, label: 'Prompt' },
    { id: 5, label: 'Game' },
    { id: 6, label: 'OnTapEvent' },
    { id: 7, label: 'MyWorld' },
  ]);
  var edges = new vis.DataSet([
    { from: 2, to: 4 },
    { from: 3, to: 5 },
    { from: 3, to: 3 },
    { from: 4, to: 5 },
    { from: 4, to: 3 },
  ]);

  toggleNetworkView(true);
  var opts = { manipulation: { editNode: editChat, initiallyActive: true } };
  new vis.Network(document.getElementById('network'),
    { nodes: nodes, edges: edges}, opts);
