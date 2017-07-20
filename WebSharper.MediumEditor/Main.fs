namespace WebSharper.MediumEditor.Extension

open WebSharper
open WebSharper.InterfaceGenerator

module Definition =

    let ToolbarOptions =
        Pattern.Config "ToolbarOptions" {
            Required = []
            Optional =
                [
                    "allowMultiParagraphSelection", T<bool>
                    "buttons", !| T<string>
                    "diffLeft", T<int>
                    "diffTop", T<int>
                    "firstButtonClass", T<string>
                    "lastButtonClass", T<string>
                    "relativeContainer", T<JavaScript.Dom.Element>
                    "standardizeSelectionStart", T<bool>
                    "static", T<bool>
                    "align", T<string>
                    "sticky", T<bool>
                    "stickyTopOffset", T<int>
                    "updateOnEmptySelection", T<bool>
                ]
        }

    let AnchorPreviewOptions =
        Pattern.Config "AnchorPreviewOptions" {
            Required = []
            Optional = 
                [
                    "hideDelay", T<int>
                    "previewValueSelector", T<string>
                    "showOnEmptyLinks", T<bool>
                    "showWhenToolbarIsVisible", T<bool>
                ]
        }

    let PlaceholderOptions =
        Pattern.Config "PlaceholderOptions" {
            Required = []
            Optional =
                [
                    "text", T<string>
                    "hideOnClick", T<bool>
                ]
        }

    let AnchorOptions =
        Pattern.Config "AnchorOptions" {
            Required = []
            Optional =
                [
                    "customClassOption", T<string>
                    "customClassOptionText", T<string>
                    "linkValidation", T<bool>
                    "placeholderText", T<string>
                    "targetCheckbox", T<bool>
                    "targetCheckboxText", T<string>
                ]
        }

    let PasteOptions =
        Pattern.Config "PasteOptions" {
            Required = []
            Optional =
                [
                    "forcePlainText", T<bool>
                    "cleanPastedHTML", T<bool>
                    "cleanReplacements", !| T<string * string>
                    "cleanAttrs", !| T<string>
                    "cleanTags", !| T<string>
                    "unwrapTags", !| T<string>
                ]
        }

    let CommandObject =
        Pattern.Config "CommandObject" {
            Required = 
                [
                    "command", T<string>
                    "key", T<string>
                    "meta", T<bool>
                    "shift", T<bool>
                ]
            Optional = []
        }

    let KeyboardCommandsOptions =
        Pattern.Config "KeyboardCommandsOptions" {
            Required = []
            Optional = 
                [
                    "commands", !| CommandObject.Type
                ]
        }

    let MediumEditorOptions =
        Pattern.Config "MediumEditorOptions" {
            Required = []
            Optional =
                [
                    "activeButtonClass", T<string>
                    "buttonLabels", T<bool>
                    "contentWindow", T<JavaScript.Window>
                    "delay", T<int>
                    "disableReturn", T<bool>
                    "disableDoubleReturn", T<bool>
                    "disableExtraSpaces", T<bool>
                    "disableEditing", T<bool>
                    "elementsContainer", T<JavaScript.Dom.Element>
                    "extensions", T<obj>
                    "ownerDocument", T<JavaScript.Dom.Document>
                    "spellcheck", T<bool>
                    "targetBlank", T<bool>
                    "toolbar", ToolbarOptions.Type + T<bool>
                    "anchorPreview", AnchorPreviewOptions.Type + T<bool>
                    "placeholder", PlaceholderOptions.Type + T<bool>
                    "anchor", AnchorOptions.Type
                    "paste", PasteOptions.Type
                    "keyboardCommands", KeyboardCommandsOptions.Type + T<bool>
                    "autoLink", T<bool>
                    "imageDragging", T<bool>
                ]
        }

    let LinkOptions =
        Pattern.Config "LinkOptions" {
            Required =
                [
                    "value", T<string>
                    "target", T<string>
                    "buttonClass", T<string>
                ]
            Optional = []
        }

    let ParseOptions =
        Pattern.Config "ParseOptions" {
            Required = []
            Optional =
                [
                    "cleanTags", !| T<string>
                    "unwrapTags", !| T<string>
                    "cleanAttrs", !| T<string>
                ]
        }

    let Elements = (T<string> + T<JavaScript.Dom.Element> + !| T<JavaScript.Dom.Element>)

    let MediumEditorClass =
        Class "MediumEditor"
        |+> Static [
            Constructor (Elements * MediumEditorOptions.Type)

            "getEditorFromElement" => T<JavaScript.Dom.Element> ^-> TSelf

            "major" =? T<JavaScript.Number>
            "minor" =? T<JavaScript.Number>
            "revision" =? T<JavaScript.Number>
            "preRelease" =? T<string>
            "toString" =? T<JavaScript.Function>
        ]
        |+> Instance [
            //Initialization Functions
            "destroy" => T<unit> ^-> T<unit>
            "setup" => T<unit> ^-> T<unit>
            "addElements" => Elements ^-> T<unit>
            "removeElements" => Elements ^-> T<unit>

            //Event Functions
            "on" => Elements * T<string> * T<JavaScript.Function> * T<bool> ^-> T<unit>
            "off" => Elements * T<string> * T<JavaScript.Function> * T<bool> ^-> T<unit>
            "subscribe" => T<string> * T<JavaScript.Function> ^-> T<unit>
            "unSubscribe" => T<string> * T<JavaScript.Function> ^-> T<unit>
            "trigger" => T<string> * (T<JavaScript.Dom.Event> + T<obj>) * T<JavaScript.Dom.Element> ^-> T<unit>

            //Selection Functions
            "checkSelection" => T<unit> ^-> T<unit>
            "exportSelection" => T<unit> ^-> T<obj>
            "importSelection" => T<obj> ^-> T<unit>
            "getFocusedElement" => T<unit> ^-> T<JavaScript.Dom.Element>
            "getSelectedParentElement" => !? T<JavaScript.Dom.Range> ^-> T<JavaScript.Dom.Element>
            "restoreSelection" => T<unit> ^-> T<unit>
            "saveSelection" => T<unit> ^-> T<unit>
            "selectAllContents" => T<unit> ^-> T<unit>
            "selectElement" => T<JavaScript.Dom.Element> ^-> T<unit>
            "stopSelectionUpdates" => T<unit> ^-> T<unit>
            "startSelectionUpdates" => T<unit> ^-> T<unit>

            //Editor Action Functions
            "cleanPaste" => T<string> ^-> T<unit>
            "createLink" => LinkOptions.Type ^-> T<unit>
            "execAction" => T<string> * LinkOptions.Type ^-> T<unit>
            "pasteHTML" => T<string> * ParseOptions.Type ^-> T<unit>
            "queryCommandState" => T<string> ^-> T<unit>

            //Helper Functions
            "checkContentChanged" => T<JavaScript.Dom.Element> ^-> T<unit>
            "delay" => T<JavaScript.Function> ^-> T<unit>
            "getContent" => !? T<int> ^-> T<JavaScript.Dom.Element>
            "getExtensionByName" => T<string> ^-> T<obj>
            "resetContent" => !? T<JavaScript.Dom.Element> ^-> T<unit>
            "serialize" => T<unit> ^-> T<obj>
            "setContent" => T<string> * !? T<int> ^-> T<unit>
        ]

    let Assembly =
        Assembly [
            Namespace "WebSharper.MediumEditor.Resources" [
                Resource "Js" "https://cdn.jsdelivr.net/medium-editor/5.23.0/js/medium-editor.min.js"
                |> AssemblyWide

                Resource "MainCSS" "https://cdn.jsdelivr.net/medium-editor/5.23.0/css/medium-editor.min.css"
                |> AssemblyWide

                Resource "BeagleCSS" "https://cdn.jsdelivr.net/medium-editor/5.23.0/css/themes/beagle.min.css"
                Resource "BootstrapCSS" "https://cdn.jsdelivr.net/medium-editor/5.23.0/css/themes/bootstrap.min.css"
                Resource "DefaultCSS" "https://cdn.jsdelivr.net/medium-editor/5.23.0/css/themes/default.min.css"
                Resource "FlatCSS" "https://cdn.jsdelivr.net/medium-editor/5.23.0/css/themes/flat.min.css"
                Resource "ManiCSS" "https://cdn.jsdelivr.net/medium-editor/5.23.0/css/themes/mani.min.css"
                Resource "RomanCSS" "https://cdn.jsdelivr.net/medium-editor/5.23.0/css/themes/roman.min.css"
                Resource "TimCSS" "https://cdn.jsdelivr.net/medium-editor/5.23.0/css/themes/tim.min.css"
            ]
            Namespace "WebSharper.MediumEditor" [
                ToolbarOptions
                AnchorOptions
                PlaceholderOptions
                AnchorPreviewOptions
                PasteOptions
                CommandObject
                KeyboardCommandsOptions
                MediumEditorOptions
                LinkOptions
                ParseOptions
                MediumEditorClass
            ]
        ]


[<Sealed>]
type Extension() =
    interface IExtension with
        member x.Assembly = Definition.Assembly

[<assembly: Extension(typeof<Extension>)>]
do ()
