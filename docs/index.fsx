(*** hide ***)

(*** condition: prepare ***)
#r "nuget: DynamicObj, 0.0.1"
#r "nuget: Newtonsoft.Json, 12.0.3"
#r "../bin/Cyjs.NET/netstandard2.1/Cyjs.NET.dll"

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

- **initialize** a `Cytoscape` object by using the respective `CyGraph.*` function .
- **attach** the elements e.g. notes and edges you want to visulize and
- further **style** the graph with fine-grained control, e.g. by setting labels, color, etc.
- **display** (in the browser or as cell result in a notebook) or save the graph (comming soon)

### Initializing a graph

The `CyGraph` module contains the `CyGraph.initEmpty` function to create an empty graph.
You can therefore initialize a cytoscape graph like this:

*)
open Cyjs.NET
let myFirstGraph = 
    CyGraph.initEmpty ()

(**

### Attach nodes and edges

The `CyGraph` module contains the `CyGraph.initEmpty` function to create an empty graph.
You can therefore initialize a cytoscape graph like this:

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

Styling functions are generally the `Chart.with*` naming convention. The following styling example does:

 - set the chart title via `Chart.withTitle`
 - set the x axis title and removes the gridline from the axis via `Chart.withX_AxisStyle`
 - set the y axis title and removes the gridline from the axis via `Chart.withY_AxisStyle`

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
                CyParam.content "data(label)"
                CyParam.color "#A00975"
            ]
    |> CyGraph.withSize(800, 400)  

(**
**Attention:** Styling functions mutate 😈 the input chart, therefore possibly affecting bindings to intermediary results. 
We recommend creating a single chart for each workflow to prevent unexpected results

### Displaying a graph in the browser

The `Chart.Show` function will open a browser window and render the input chart there. When working in a notebook context, after
[referencing Plotly.NET.Interactive](#For-dotnet-interactive-notebooks), the function is not necessary, just end the cell with the value of the chart.

*)

(***do-not-eval***)
myGraph
|> CyGraph.show

(**Should render this chart in your brower:*)

(***hide***)
myGraph |> CyGraph.withSize(800, 400) |> HTML.toEmbeddedHTML
(*** include-it-raw ***)

(***do-not-eval***)
myFirstStyledGraph
|> CyGraph.show

(**And here is what happened after applying the styles from above:*)

(***hide***)
myFirstStyledGraph |> HTML.toEmbeddedHTML
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