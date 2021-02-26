namespace Cyjs.NET

open Cyjs.NET.CytoscapeModel
open CyParam
open DynamicObj

// Module to manipulate graph elements like node and edges
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


// Module to manipulate and sytely a graph
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

    let withSize (width:int,height:int) (cy:CyGraph) : CyGraph =
        let width  = CyParam.width (sprintf "%ipx" width )
        let height = CyParam.height (sprintf "%ipx" height )        
        let canvas = 
            let tmp = cy.TryGetTypedValue<Canvas> "canvas" // DynamicObj.DynObj.tryGetValue cy "Dims" //tryGetLayoutSize gChart       
            match tmp with
            |Some c -> c
            |None -> Canvas()
        width.Value  |> DynObj.setValue canvas width.Name
        height.Value |> DynObj.setValue canvas height.Name
        canvas?display <- "block"
        cy?Canvas <- canvas
        cy


    let withCanvas (styles:seq<CyStyleParam>) (cy:CyGraph) : CyGraph = 
        let canvas = 
            let tmp = cy.TryGetTypedValue<Canvas> "canvas" // DynamicObj.DynObj.tryGetValue cy "Dims" //tryGetLayoutSize gChart       
            match tmp with
            |Some c -> c
            |None -> Canvas()
        canvas.WithStyle styles
        cy?Canvas <- canvas
        cy


    let show (cy:CyGraph)  =
        HTML.show cy



