(*** hide ***)

(*** condition: prepare ***)

#r "nuget: Newtonsoft.Json, 13.0.1"
#r "nuget: DynamicObj, 2.0.0"
#r "nuget: Giraffe.ViewEngine, 1.4.0"
#r "../src/Cyjs.NET/bin/Release/netstandard2.0/Cyjs.NET.dll"

open Cyjs.NET
Defaults.DefaultDisplayOptions <- DisplayOptions.init(CytoscapeJSReference = CytoscapeJSReference.NoReference)

(*** condition: ipynb ***)
#if IPYNB
#r "nuget: Cyjs.NET, {{fsdocs-package-version}}"
#endif // IPYNB


(**
# Cyjs.NET

[![Binder](https://mybinder.org/badge_logo.svg)](https://mybinder.org/v2/gh/fslaborg/Cyjs.NET/gh-pages?filepath=index.ipynb)

Cyjs.NET is an interface for Cytoscape.js written in F# to visualiz complex networks and integrate these with any type of attribute data.

### Table of contents 

- [Installation](#Installation)
    - [For applications and libraries](#For-applications-and-libraries)
    - [For scripting](#For-scripting)
- [Overview](#Overview)
    - [Basics](#Basics)
        - [Initializing a graph](#Initializing-a-graph)
        - [Attaching nodes and edges](#Attaching-nodes-and-edges)
        - [Styling a graph](#Styling-a-graph)
        - [Displaying a graph](#Displaying-a-graph)
- [Contributing and copyright](#Contributing-and-copyright)

# Installation


## Installation

The following examples show how easy it is to start working with Cyjs.NET.

### For applications and libraries

You can get all Cyjs.NET packages from nuget at [nuget page](https://www.nuget.org/packages/Cyjs.NET/).


 - dotnet CLI

```shell
dotnet add package Cyjs.NET --version <desired-version-here>
```

Or add the package reference directly to your `.*proj` file:

```
<PackageReference Include="Cyjs.NET" Version="<desired-version-here>" />
```

### For scripting

You can include the package via an inline package reference:

```
#r "nuget: Cyjs.NET, <desired-version-here>"
```

# Overview

## Basics

The general design philosophy of Cyjs.NET implements the following visualization flow:

- **initialize** a `Cytoscape` object by using the `CyGraph.initEmpty` function.
- **attach** elements e.g. notes, edges and data to visulize and further
- **style** the graph with fine-grained control, e.g. by setting labels, color, etc.
- **display** (in the browser or as cell result in a notebook) or save the graph (comming soon)

### Initializing a graph

The `CyGraph` module contains the `CyGraph.initEmpty` function to create an empty graph.
You can therefore initialize a cytoscape graph like this:

*)
open Cyjs.NET
let myFirstGraph = 
    CyGraph.initEmpty ()

(***include-it-raw***)
"LOL"

(**

### Attach nodes and edges

The `Elements` module contains the `node` and `edge` functions to create the respective element.
Node and edges can be decorated with data as `CyStyleParam list`
You can therefore create a cytoscape graph with two nodes and an edge like this:

*)
open Elements

let myGraph = 
    CyGraph.initEmpty ()
    |> CyGraph.withElements [
            node "n1" [ CyParam.label "FsLab"  ]
            node "n2" [ CyParam.label "ML" ]
 
            edge  "e1" "n1" "n2" []
        ]
(**

### Styling a graph

Styling functions are generally the `CyGraph.with*` naming convention. The following styling example does:

 - add two nodes including a text label
 - add an edge without any additional data 
 - styles the nodes with color and content via `CyGraph.withStyle`
 - sets the graph size to 800 x 400 pixels via `CyGraph.withSize`

*)

let myFirstStyledGraph =     
    CyGraph.initEmpty ()
    |> CyGraph.withElements [
            node "n1" [ CyParam.label "FsLab"  ]
            node "n2" [ CyParam.label "ML" ]
 
            edge  "e1" "n1" "n2" []
 
        ]
    |> CyGraph.withStyle "node"     
            [
                CyParam.content =. CyParam.label
                CyParam.color "#A00975"
            ]
    |> CyGraph.withSize(800, 400)  
(**
**Attention:** `=.` is a styling mapper and allows to pass data from different sources into the layout. 
Here the label attached to each node is rendered as content.  

### Displaying a graph in the browser

The `CyGraph.show` function will open a browser window and render the input graph there.

*)

(***do-not-eval***)
myGraph
|> CyGraph.show

(**Should render this chart in your brower:*)

(***hide***)
myGraph 
|> CyGraph.withZoom(CytoscapeModel.Zoom.Init(ZoomingEnabled=false)) 
|> CyGraph.withSize(800, 400) 
|> HTML.toGraphHTML()
(*** include-it-raw ***)

(***do-not-eval***)
myFirstStyledGraph
|> CyGraph.show

(**And here is what happened after applying the styles from above:*)

(***hide***)
myFirstStyledGraph |> CyGraph.withZoom(CytoscapeModel.Zoom.Init(ZoomingEnabled=false)) |> HTML.toGraphHTML()
(*** include-it-raw ***)


(**
# Contributing and copyright

The project is hosted on [GitHub][gh] where you can [report issues][issues], fork 
the project and submit pull requests. If you're adding a new public API, please also 
consider adding [samples][content] that can be turned into a documentation. You might
also want to read the [library design notes][readme] to understand how it works.

The library is available under Public Domain license, which allows modification and 
redistribution for both commercial and non-commercial purposes. For more information see the 
[License file][license] in the GitHub repository. 

  [content]: https://github.com/fslaborg/Cyjs.NET/tree/master/docs/content
  [gh]: https://github.com/fslaborg/Cyjs.NET
  [issues]: https://github.com/fslaborg/Cyjs.NET/issues
  [readme]: https://github.com/fslaborg/Cyjs.NET/blob/master/README.md
  [license]: https://github.com/fslaborg/Cyjs.NET/blob/master/LICENSE.txt
*)