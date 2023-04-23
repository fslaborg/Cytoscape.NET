﻿module Globals

open DynamicObj
open System.Runtime.InteropServices
open Newtonsoft.Json
open Giraffe.ViewEngine

/// The cytoscape js version loaded from cdn in rendered html docs
[<Literal>]
let CYTOSCAPEJS_VERSION = "3.24.0"

[<Literal>]
let SCRIPT_TEMPLATE = """
var graphdata = [GRAPHDATA]
var cy = cytoscape( graphdata );
cy.userZoomingEnabled( [ZOOMING] );
"""

[<Literal>]
let REQUIREJS_SCRIPT_TEMPLATE = """
var fsharpCytoscapeRequire = requirejs.config({context:'fsharp-cyjs',paths:{cytoscape:'[REQUIRE_SRC]'}}) || require;
        
fsharpCyjsRequire(['cytoscape'], function(cytoscape) {
    var graphdata = [GRAPHDATA]
    var cy = cytoscape( graphdata );
    cy.userZoomingEnabled( [ZOOMING] );
});
"""

/// base64 encoded favicon logo for generated htmls
[<Literal>]
let internal LOGO_BASE64 = """iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAIAAAAlC+aJAAAEtWlUWHRYTUw6Y29tLmFkb2JlLnhtcAAAAAAAPD94cGFja2V0IGJlZ2luPSLvu78iIGlkPSJXNU0wTXBDZWhpSHpyZVN6TlRjemtjOWQiPz4KPHg6eG1wbWV0YSB4bWxuczp4PSJhZG9iZTpuczptZXRhLyIgeDp4bXB0az0iWE1QIENvcmUgNS41LjAiPgogPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4KICA8cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0iIgogICAgeG1sbnM6dGlmZj0iaHR0cDovL25zLmFkb2JlLmNvbS90aWZmLzEuMC8iCiAgICB4bWxuczpleGlmPSJodHRwOi8vbnMuYWRvYmUuY29tL2V4aWYvMS4wLyIKICAgIHhtbG5zOnBob3Rvc2hvcD0iaHR0cDovL25zLmFkb2JlLmNvbS9waG90b3Nob3AvMS4wLyIKICAgIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIKICAgIHhtbG5zOnhtcE1NPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvbW0vIgogICAgeG1sbnM6c3RFdnQ9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZUV2ZW50IyIKICAgdGlmZjpJbWFnZUxlbmd0aD0iNjQiCiAgIHRpZmY6SW1hZ2VXaWR0aD0iNjQiCiAgIHRpZmY6UmVzb2x1dGlvblVuaXQ9IjIiCiAgIHRpZmY6WFJlc29sdXRpb249IjE1MC8xIgogICB0aWZmOllSZXNvbHV0aW9uPSIxNTAvMSIKICAgZXhpZjpQaXhlbFhEaW1lbnNpb249IjY0IgogICBleGlmOlBpeGVsWURpbWVuc2lvbj0iNjQiCiAgIGV4aWY6Q29sb3JTcGFjZT0iMSIKICAgcGhvdG9zaG9wOkNvbG9yTW9kZT0iMyIKICAgcGhvdG9zaG9wOklDQ1Byb2ZpbGU9InNSR0IgSUVDNjE5NjYtMi4xIgogICB4bXA6TW9kaWZ5RGF0ZT0iMjAyMy0w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"""

///
let internal JSON_CONFIG =
    JsonSerializerSettings(ReferenceLoopHandling = ReferenceLoopHandling.Serialize)