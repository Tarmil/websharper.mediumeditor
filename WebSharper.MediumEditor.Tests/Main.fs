namespace WebSharper.MediumEditor.Tests

open WebSharper
open WebSharper.MediumEditor
open WebSharper.Sitelets
open WebSharper.UI
open WebSharper.UI.Client
open WebSharper.UI.Html

[<JavaScript>]
module Client =
    open WebSharper.JavaScript
    open WebSharper.Testing

    let Tests =
        TestCategory "General" {

            Test "Sanity check" {
                equalMsg (1 + 1) 2 "1 + 1 = 2"
            }

            Test "Constructor test" {
                let editor = div [] []
                notEqualMsg (MediumEditor(editor.Dom, MediumEditorOptions())) (JS.Undefined) "Medium Editor constructor"
            }
        }

    [<SPAEntryPoint>]
    let RunTests() =
        let e = Runner.RunTests [Tests]
        e.ReplaceInDom(JS.Document.QuerySelector "#body")
