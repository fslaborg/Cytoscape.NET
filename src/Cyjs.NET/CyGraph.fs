namespace Cyjs.NET



module Elements =

    type Node = Element
    type Edge = Element


    let node id (dataAttributes:list<CssStyle>) : Node =
        let data = 
            Css.applyCssStyles (Data.init id) dataAttributes 
        Element.init(Data = data)    

    let edge id sourceId targetId (dataAttributes:list<CssStyle>) : Edge =
        let data = 
            let tmp = (Data.init (id,sourceId,targetId))
            Css.applyCssStyles tmp dataAttributes  
        Element.init(Data = data)
    
    let withClass (cssClass:CssClass) (elem:#Element) =
        elem.AddClass cssClass
        elem


module CyGraph =

    type CyGraph = Cytoscape

    let initEmpty () = CyGraph()

    let withElements (elems:seq<Element>) (cy:CyGraph) : CyGraph =
        for elem in elems do
            cy.AddElement elem
        cy 
    
    //(selector:string)
    let withStyle (selector:string) (cssStyles:seq<CssStyle>) (cy:CyGraph) : CyGraph =
            let style = Style.init(selector,cssStyles |> Seq.toList)
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



