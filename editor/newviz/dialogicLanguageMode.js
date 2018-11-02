// define Language Mode
  CodeMirror.defineSimpleMode("dialogic",
  {
    // The start state contains the rules that are initially used
    start: [

      // The regex matches the token, the token property contains the type
      {
        regex: /"(?:[^\\]|\\.)*?(?:"|$)/,
        token: "string"
      },

      // You can match multiple tokens at once. Note that the captured
      // groups must span the whole string in this case
      {
        regex: /(function)(\s+)([a-z$][\w$]*)/,
        token: ["keyword", null, "variable-2"]
      },

      // Rules are matched in the order in which they appear, so there is
      // no ambiguity between this one and the one above
      // ! DIALOGIC
      {
        regex: /CHAT(?= )/,
        token: "command-chat",
        next: "labelafterchat"
      }, // Positive look behind does not work in javascript
      {
        regex: /(?:CHAT|SAY|ASK|OPT|DO|GO|FIND|WAIT|SET)\b/,
        token: "command"
      },
      {
        regex: /([^=]*):/,
        token: "actor"
      },

      {
        regex: /\$.+?(?=\s|$|\.)\./,
        token: "variable",
        next: "transformation"
      },
      {
        regex: /(\(.+\|.+\))\./,
        token: "group",
        next: "transformation"
      },
      {
        regex: /\$.+?(?=\s|$|\.)/,
        token: "variable"
      },
      {
        regex: /(\(.+\|.+\))/,
        token: "group"
      },

      // {regex: /\(.+\)(\..+(.*))/, token: "transformation"},
      //  {regex: /\$.+(\..+(.*))/, token: "transformation"},

      {
        regex: /\#(_|[a-zA-Z]).*?(?=\s|$)/,
        token: "label"
      },
      {
        regex: /{/,
        token: "metadata",
        next: "metadata"
      },

      // ! DIALOGIC
      {
        regex: /true|false|null|undefined/,
        token: "atom"
      },

      // {regex: /0x[a-f\d]+|[-+]?(?:\.\d+|\d+\.?\d*)(?:e[-+]?\d+)?/i, token: "number"},
      {
        regex: /\/\/.*/,
        token: "comment"
      },

      // A next property will cause the mode to move to a different state
      {
        regex: /\/\*/,
        token: "comment",
        next: "comment"
      },
      {
        regex: /[-+\/*=<>!^()|]+/,
        token: "operator"
      },

      // indent and dedent properties guide autoindentation
      {
        regex: /[\{\[\(]/,
        indent: true
      },
      {
        regex: /[\}\]\)]/,
        dedent: true
      }
    ],

    // DIALOGIC: hack - label after chat
    labelafterchat: [
    {
      regex: / .+?\b/,
      token: "label",
      next: "start"
    }],

    // group:[
    //     {regex: /\$.+?(?=\s|\||\))/, token: "variable"},
    //     {regex: /[^$]*?\)/, token: "group", next: "start"},
    //     {regex: /[^$]*/g, token: "group"}
    // ],

    // DIALOGIC: metadata state
    metadata: [
    {
      regex: /}/,
      token: "metadata",
      next: "start"
    },
    {
      regex: /\w+(?= *(=|>=|<=|>|<|!=|\$=|\^=|\*=))/,
      token: "key"
    },
    {
      regex: /=|<|>|!|\$|\^|\*|!|,| /,
      token: "metadata"
    },
    {
      regex: /.*?(?=,| ,|}| })/,
      token: "value"
    }],
    transformation: [
    {
      regex: /.+\(.*\)/,
      token: "transformation",
      next: "start"
    },
    {
      regex: /.*/,
      token: "transformation"
    }],

    // The multi-line comment state.
    comment: [
    {
      regex: /.*?\*\//,
      token: "comment",
      next: "start"
    },
    {
      regex: /.*/,
      token: "comment"
    }],

    // The meta property contains global information about the mode. It
    // can contain properties like lineComment, which are supported by
    // all modes, and also directives like dontIndentStates, which are
    // specific to simple modes.
    meta:
    {
      dontIndentStates: ["comment"],
      lineComment: "//"
    }
  });
