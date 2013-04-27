namespace Website

open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Html

type D3Examples() =
    inherit Web.Control()

    [<JavaScript>]
    override this.Body = Div [] :> _