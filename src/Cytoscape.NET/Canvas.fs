namespace Cytoscape.NET.CytoscapeModel

open System
open DynamicObj
open Cytoscape.NET

    
type Canvas() = 
    inherit DynamicObj ()
    
    /// Init a default canvas for responsive rendering ( width: 100%; height: 100%; position: absolute; top: 0px; left: 0px; )
    static member InitDefault() =
          let c = Canvas()
          c?width <- "100%"
          c?height <- "100%"
          c?position <- "absolute"
          c?top <- "0px"
          c?left <- "0px"
          c

    member this.WithStyle (styles : CyParam.CyStyleParam seq) =
          for item in styles do
               item.Value |> DynObj.setValue this item.Name

     
