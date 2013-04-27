module Website.D3

open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Html
type d3 = Wrappers.D3.d3

[<JS>]
let paintBodyBlack() = d3.Select("body").Style("background-color", "black")

type Examples() =
    inherit Web.Control()

    [<JS>]
    override this.Body =
        paintBodyBlack()
        Div [] :> _