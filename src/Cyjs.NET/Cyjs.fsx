#r "nuget: DynamicObj, 0.0.1"
#r "nuget: Newtonsoft.Json, 12.0.3"
#r "../..//bin/Cyjs.NET/netstandard2.1/Cyjs.NET.dll"

open Cyjs.NET
open Elements

CyGraph.initEmpty ()
|> CyGraph.withElements [
        node "j" [ Css.label "Jerry"  ; Css.weight 65; Css.color "#6FB1FC"; Css.shape "triangle"  ]
        node "e" [ Css.label "Elaine" ; Css.weight 45; Css.color "#EDA1ED"; Css.shape "ellipse"  ]
        node "k" [ Css.label "Kramer" ; Css.weight 75; Css.color "#86B342"; Css.shape "octagon"  ]
        node "g" [ Css.label "George" ; Css.weight 70; Css.color "#F5A45D"; Css.shape "rectangle"  ]

        edge  "1" "j" "e" [Css.color "#6FB1FC"; Css.weight 90]
        edge  "2" "j" "e" [Css.color "#6FB1FC"; Css.weight 120]
        edge  "3" "j" "k" [Css.color "#6FB1FC"; Css.weight 70]
        edge  "4" "j" "g" [Css.color "#6FB1FC"; Css.weight 80]
 
        edge  "5" "e" "j" [Css.color "#EDA1ED"; Css.weight 95]
        edge  "6" "e" "k" [Css.color "#EDA1ED"; Css.weight 60] 
            |> withClass (Css.cssClass "questionable") // lasses: 'questionable'
         
        edge  "7" "k" "j" [Css.color "#86B342"; Css.weight 100]
        edge  "8" "k" "e" [Css.color "#86B342"; Css.weight 100]
        edge  "9" "k" "g" [Css.color "#86B342"; Css.weight 100]
 
        edge "10" "g" "j" [Css.color "#F5A45D"; Css.weight 90]
        edge "11" "g" "g" [Css.color "#F5A45D"; Css.weight 90]
        edge "12" "g" "g" [Css.color "#F5A45D"; Css.weight 90]
        edge "13" "g" "g" [Css.color "#F5A45D"; Css.weight 90]

    ]
|> CyGraph.withStyle "node"     
        [
            Css.shape "data(shape)"
            Css.width "mapData(weight, 40, 80, 20, 60)"
            Css.content "data(label)"
            Css.text.align.center
            Css.text.outline.width 2
            Css.text.outline.color "data(color)"
            Css.background.color "data(color)"
            Css.color "#fff"
        ]
|> CyGraph.withStyle ":selected"     
        [
            Css.border.width 3
            Css.border.color "#333"
        ]
|> CyGraph.withStyle "edge"     
        [
            Css.curve.style "bezier"
            Css.opacity 0.666
            Css.width "mapData(weight, 70, 100, 2, 6)"
            Css.target.arrow.shape "triangle"
            Css.source.arrow.shape "circle"
            Css.line.color "data(color)"
            Css.target.arrow.color "data(color)"
            Css.source.arrow.color "data(color)"
        ]
|> CyGraph.withStyle "edge.questionable"     
        [
            Css.line.style "dotted"
            Css.target.arrow.shape "diamond"
        ]
|> CyGraph.withStyle ".faded"     
        [
            Css.opacity 0.666
            Css.text.opacity 0
        ]
|> CyGraph.withLayout (Layout.init("cose"))        
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