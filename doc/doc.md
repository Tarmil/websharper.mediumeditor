# Medium Editor

[Medium Editor](http://yabwe.github.io/medium-editor/) grants a new, easy to use text editor written in JavaScript. The library aims to replicate [Medium.com](medium.com)'s text editor which is a big blog website. 

## Getting started

To set up the extension all we need is a target div (or text field if we want to keep our formatted text in the DOM), and initializing the MediumEditor from F#.

In the html file we need an element with a class, for example "editable":

```html
<textarea class="editable"></textarea>
```

If we have this, we can access this with our MediumEditor like this:

```fsharp
let editor = MediumEditor(".editable")
```

With this, we're ready to use our default editor.

## Configure Medium Editor

The editor comes with a lot of settings. We can set it up in its constructor with the `MediumEditorOptions` record type, like this:

```fsharp
let editor =
    MediumEditor(
        ".editable",
        MediumEditorOptions(
            ActiveButtonClass = "medium-editor-button-active",
            AllowMultiParagraphSelection = true,
            ButtonLabels = false,
            Delay = 0,
            DisableReturn = false,
            DisableDoubleReturn = false,
            DisableExtraSpaces = false,
            DisableEditing = false
        )
    )
```

See the options in the [original documentation](https://github.com/yabwe/medium-editor/blob/master/OPTIONS.md).

Every field where we have to give an object has its own record type. If the element could be disabled by giving a `false` value, we have to use Unions. This table shows the field-record pairs and the Union type if it needs one:

| Field              | Record type               | Has union |
|--------------------|---------------------------|-----------|
| `toolbar`          | `ToolbarOptions`          | Union1Of2 |
| `anchor`           | `AnchorOptions`           | Union1Of2 |
| `placeholder`      | `PlaceholderOptions`      | Union1Of2 |
| `anchorPreview`    | `AnchorPreviewOptions`    |           |
| `paste`            | `PasteOptions`            |           |
| `keyboardCommands` | `KeyboardCommandsOptions` | Union1Of2 |
| `link`             | `LinkOptions`             |           |
| `parse`            | `ParseOptions`            |           |