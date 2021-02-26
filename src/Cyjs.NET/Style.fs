namespace Cyjs.NET.CytoscapeModel

open DynamicObj
open Cyjs.NET

// https://js.cytoscape.org/#style

// http://jsfiddle.net/todaylg/f6oj27am/

type Style() =
    inherit DynamicObj ()

    static member Init 
        (
            selector:string,
            styles : CyParam.CyStyleParam list
            ) =
        let s     = Style()
        let inner = Style()
        selector |> DynObj.setValue s "selector"
        for item in styles do
            item.Value |> DynObj.setValue inner item.Name

        inner |> DynObj.setValue s "style"
        s
