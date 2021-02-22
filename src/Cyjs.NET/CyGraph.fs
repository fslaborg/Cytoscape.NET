namespace Cyjs.NET

open Cyjs.NET.CytoscapeModel
open CyParam

module Elements =
    
    type Node = Element
    type Edge = Element


    let node id (dataAttributes:list<CyStyleParam>) : Node =
        let data = 
            CyParam.applyCyStyles (Data.init id) dataAttributes 
        Element.init(Data = data)    

    let edge id sourceId targetId (dataAttributes:list<CyStyleParam>) : Edge =
        let data = 
            let tmp = (Data.init (id,sourceId,targetId))
            CyParam.applyCyStyles tmp dataAttributes  
        Element.init(Data = data)
    
    let withClass (cyClass:CyStyleClass) (elem:#Element) =
        elem.AddClass cyClass
        elem


module CyGraph =
    open CyParam

    type CyGraph = Cytoscape

    let initEmpty () = CyGraph()

    let withElements (elems:seq<Element>) (cy:CyGraph) : CyGraph =
        for elem in elems do
            cy.AddElement elem
        cy 
    
    //(selector:string)
    let withStyle (selector:string) (cyStyles:seq<CyStyleParam>) (cy:CyGraph) : CyGraph =
            let style = Style.init(selector,cyStyles |> Seq.toList)
            cy.AddStyle style
            cy 

    let withStyles (styles:seq<Style>) (cy:CyGraph) : CyGraph =
            for style in styles do
                cy.AddStyle style
            cy 

    let withLayout (ly:Layout) (cy:CyGraph) : CyGraph = 
        cy.layout <- ly
        cy


    let show (cy:CyGraph) =
        HTML.show cy



