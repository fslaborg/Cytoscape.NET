#r "nuget: DynamicObj, 0.0.1"
#r "nuget: Newtonsoft.Json, 12.0.3"
#r "../..//bin/Cyjs.NET/netstandard2.1/Cyjs.NET.dll"

open Cyjs.NET
open Elements

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
            |> withClass (CyParam.cyClass "questionable") // lasses: 'questionable'
         
        edge  "7" "k" "j" [CyParam.color "#86B342"; CyParam.weight 100]
        edge  "8" "k" "e" [CyParam.color "#86B342"; CyParam.weight 100]
        edge  "9" "k" "g" [CyParam.color "#86B342"; CyParam.weight 100]
 
        edge "10" "g" "j" [CyParam.color "#F5A45D"; CyParam.weight 90]
        edge "11" "g" "g" [CyParam.color "#F5A45D"; CyParam.weight 90]
        edge "12" "g" "g" [CyParam.color "#F5A45D"; CyParam.weight 90]
        edge "13" "g" "g" [CyParam.color "#F5A45D"; CyParam.weight 90]

    ]
|> CyGraph.withStyle "node"     
        [
            CyParam.shape "data(shape)"
            CyParam.width "mapData(weight, 40, 80, 20, 60)"
            CyParam.content "data(label)"
            CyParam.Text.Align.center
            CyParam.Text.Outline.width 2
            CyParam.Text.Outline.color "data(color)"
            CyParam.Background.color "data(color)"
            CyParam.color "#fff"
        ]
|> CyGraph.withStyle ":selected"     
        [
            CyParam.Border.width 3
            CyParam.Border.color "#333"
        ]
|> CyGraph.withStyle "edge"     
        [
            CyParam.Curve.style "bezier"
            CyParam.opacity 0.666
            CyParam.width "mapData(weight, 70, 100, 2, 6)"
            CyParam.Target.Arrow.shape "triangle"
            CyParam.Source.Arrow.shape "circle"
            CyParam.Line.color "data(color)"
            CyParam.Target.Arrow.color "data(color)"
            CyParam.Source.Arrow.color "data(color)"
        ]
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
|> CyGraph.withLayout (CytoscapeModel.Layout.Init("cose"))        
|> CyGraph.withSize(800, 800)
|> CyGraph.withZoom(CytoscapeModel.Zoom.Init(ZoomingEnabled=false)) 
|> CyGraph.show






// let n1 = Element.init(Data = (Data.init "n1"))
// let n2 = Element.init(Data = (Data.init "n2"))
// let e  = Element.init(Data = (Data.init ("e", "n1", "n2")))

// // n1.AddClass (Css.cssClass "questionable")


// let ly = [
//     Css.shape "hexagon"    
//     Css.style "background-color" "red"
//     Css.style "label" "data(id)"
//     ]
// let sy = Style.init ("node", ly)

// let graph =
//     let cy = Cytoscape()
//     cy.AddElement(n1)
//     cy.AddElement(n2)
//     cy.AddElement(e)
//     cy.AddStyle sy
//     cy

// HTML.show graph
