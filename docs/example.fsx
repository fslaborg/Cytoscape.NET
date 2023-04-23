(*** hide ***)

(*** condition: prepare ***)
#r "nuget: Newtonsoft.Json, 13.0.1"
#r "nuget: DynamicObj, 2.0.0"
#r "nuget: Giraffe.ViewEngine, 1.4.0"
#r "../src/Cyjs.NET/bin/Release/netstandard2.0/Cyjs.NET.dll"

open Cyjs.NET
Defaults.DefaultDisplayOptions <- DisplayOptions.init(CytoscapeJSReference = CytoscapeJSReference.NoReference)


(**
# More Complex Example Graph

Here, we can look at a more colorful example to understand the functionality of Cyjs.NET. The example is translated from [here](www.w.de)
to cover different styling capabilities. 

### Adding nodes and edges with data

After including the necessary dependancies, we can add the elements with data to our graph.
Additionaly, the `withClass` function adds a class identifier `(CyParam.cyClass "questionable")` to the respective edge.
This can be used for individual styling, that you will see later...  
*)

open Cyjs.NET
open Elements

let complexGraph = 
    CyGraph.initEmpty ()
    |> CyGraph.withElements [
            node "j" [ CyParam.label "Jerry"  ; CyParam.weight 65; CyParam.color "#6FB1FC"; CyParam.shape "triangle"  ]
            node "e" [ CyParam.label "Elaine" ; CyParam.weight 45; CyParam.color "#EDA1ED"; CyParam.shape "ellipse"  ]
            node "k" [ CyParam.label "Kramer" ; CyParam.weight 75; CyParam.color "#86B342"; CyParam.shape "octagon"  ]
            node "g" [ CyParam.label "George" ; CyParam.weight 70; CyParam.color "#F5A45D"; CyParam.shape "rectangle"  ]

            edge  "1" "j" "e" [CyParam.color "#6FB1FC"; CyParam.weight 90]
            edge  "2" "j" "e" [CyParam.color "#6FB1FC"; CyParam.weight 120]
            edge  "3" "j" "k" [CyParam.color "#6FB1FC"; CyParam.weight 70]
            edge  "4" "j" "g" [CyParam.color "#6FB1FC"; CyParam.weight 80]
     
            edge  "5" "e" "j" [CyParam.color "#EDA1ED"; CyParam.weight 95]
            edge  "6" "e" "k" [CyParam.color "#EDA1ED"; CyParam.weight 60] 
                |> withClass (CyParam.cyClass "questionable") // classes: 'questionable'
             
            edge  "7" "k" "j" [CyParam.color "#86B342"; CyParam.weight 100]
            edge  "8" "k" "e" [CyParam.color "#86B342"; CyParam.weight 100]
            edge  "9" "k" "g" [CyParam.color "#86B342"; CyParam.weight 100]
     
            edge "10" "g" "j" [CyParam.color "#F5A45D"; CyParam.weight 90]
            edge "11" "g" "g" [CyParam.color "#F5A45D"; CyParam.weight 90]
            edge "12" "g" "g" [CyParam.color "#F5A45D"; CyParam.weight 90]
            edge "13" "g" "g" [CyParam.color "#F5A45D"; CyParam.weight 90]

        ]

(**
### Styling nodes

Using the `withStyle` function we use a selector string (here: node) to specify the target that we want to style.
Second, we provide a list of style paramerts `CyStyleParam` to set the design.
**attention** Style mapper `=.` specifies a direct mapping or a linear mapping `<=.` to an element’s data field. 
*)
let cgStylingNodes = 
    complexGraph
    |> CyGraph.withStyle "node"     
            [
                CyParam.shape =. CyParam.shape
                // Style mapper can also be used untyped in string form: CyParam.shape "data(shape)"
                CyParam.width <=. (CyParam.weight, 40, 80, 20, 60)
                // A linear style mapper like this: CyParam.width "mapData(weight, 40, 80, 20, 60)"
                CyParam.content =. CyParam.label
                CyParam.Text.Align.center
                CyParam.Text.Outline.width 2
                CyParam.Text.Outline.color =. CyParam.color  //=. CyParam.color
                CyParam.Background.color   =. CyParam.color  //=. CyParam.color
                CyParam.color "#fff"
            ]  
(**
### Interactivity

Here, the active selector `:selected` is used to select and style the activly selected node.  
*)
let cgInteractivity =
    cgStylingNodes
    |> CyGraph.withStyle ":selected"     
            [
                CyParam.Border.width 3
                CyParam.Border.color "#333"
            ]
(**
### Styling edges

Styling the edges is analogous to adapting the node style. 

*)
let cgStylingEdges = 
    cgInteractivity
    |> CyGraph.withStyle "edge"     
            [
                CyParam.Curve.style "bezier"
                CyParam.opacity 0.666
                CyParam.width <=. (CyParam.weight, 70, 100, 2, 6)
                CyParam.Target.Arrow.shape "triangle"
                CyParam.Source.Arrow.shape "circle"
                CyParam.Line.color =. CyParam.color
                CyParam.Target.Arrow.color =. CyParam.color
                CyParam.Source.Arrow.color =. CyParam.color
            ]
(**
### Edge styling with class identifier
The class identifier `questionable` can be used to select and style the respective edge(s).  
*)
let cgStylingEdgesWci = 
    cgStylingEdges
    |> CyGraph.withStyle "edge.questionable"     
            [
                CyParam.Line.style "dotted"
                CyParam.Target.Arrow.shape "diamond"
            ]
    |> CyGraph.withStyle ".faded"     
            [
                CyParam.opacity 0.666
                CyParam.Text.opacity 0
            ]
(**
### Using a graph layout

To draw the graph nicly, `withLayout` applies CoSE (Compound graph Spring Embedder) layout, an algorithm based 
on the traditional force-directed layout scheme with extensions to handle multi-level nesting, edges between nodes 
of arbitrary nesting level, varying node sizes, and other possible application-specific constraints.

Layouts can be specified with `LayoutOptions` applied. Here, we use `ComponentSpacing`, which adds
extra spacing between components in non-compound graphs.
*)
let cgLayout = 
    cgStylingEdgesWci
    |> CyGraph.withLayout (
        Layout.initCose (Layout.LayoutOptions.Cose(ComponentSpacing=40)) 
        )  
    |> CyGraph.withSize(800, 800) 

(***do-not-eval***)
cgLayout
|> CyGraph.show

(**And here is what happened after applying the styles from above:*)

(***hide***)
cgLayout |> HTML.toEmbeddedHTML()
(*** include-it-raw ***)    