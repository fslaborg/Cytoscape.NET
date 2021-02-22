namespace Cyjs.NET

type Node = Element
type Edge = Element


module CyGraph =

    let initEmpty = Cytoscape()

    let node id (dataAttributes:list<CssStyle>) : Node =
        let data = 
            Css.applyCssStyles (Data.init id) dataAttributes 
        Element.init(Data = data)    

    let edge id sourceId targetId (dataAttributes:list<CssStyle>) : Edge =
        let data = 
            let tmp = (Data.init (id,sourceId,targetId))
            Css.applyCssStyles tmp dataAttributes  
        Element.init(Data = data) 
