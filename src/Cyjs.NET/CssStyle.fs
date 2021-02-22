namespace Cyjs.NET

open System
open DynamicObj

type CssStyle = {
  Name        : string
  Value       : obj
}

type CssClass = {
  Class       : string
}


module Css =

    let applyCssStyle (dyObj:#DynamicObj) (style:CssStyle) =
        style.Value |> DynObj.setValue dyObj style.Name
        dyObj

    let applyCssStyles (dyObj:#DynamicObj) (styles:seq<CssStyle>) =
        styles
        |> Seq.iter (fun style -> 
            style.Value |> DynObj.setValue dyObj style.Name
            )
        dyObj
        


    let inline style name v  = {
        Name = name
        Value = v
    }

    let inline cssClass name = { Class = name }

    let color v   = style "color" v
    let content v = style "content" v
    let label v   = style "label" v
    let opacity v = style "opacity" v
    
    let shape v   = style "shape" v
    let weight v  = style "weight" v
    let width v   = style "width" v
    
    module background =
        let color v   = style "background-color" v

    module border =
        let color v   = style "border-color" v                
        let width v   = style "border-width" v

    module curve =
        let style v   = style "curve-style" v

    module line =
        let color v   = style "line-color" v
        let style v   = style "border-style" v

    module source =
        module arrow =
            let shape v = style "source-arrow-shape" v
            let color v = style "source-arrow-color" v

    module target =     
        module arrow =
            let shape v = style "target-arrow-shape" v
            let color v = style "target-arrow-color" v

    module text =        
        module align =
            let center = style "text-valign" "center"
            let left = style "text-valign" "left"
            let right = style "text-valign" "right"
        module outline =
            let width v = style "text-outline-width" v
            let color v = style "text-outline-color" v
        let opacity v = style "text-opacity" v


