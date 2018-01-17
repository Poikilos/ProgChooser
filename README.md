# ProgChooser
Extension disambiguator to choose the right program despite Windows (for now, only associate SLN files with this)

## Known Issues
* Solution format 10.0 doesn't load in SharpDevelop 3, so:
  * detect this problem
  * load earliest version though, or at least mark earliest version in list (recommend best version even if not present)

## Primary Goals
* Make Windows open the program you choose with one click (after you double click an ambiguous file) such as for SLN since it used for multiple languages and IDEs. This is basically the WIndows equivalent of my filehandoff project, you need something to unconfuse Windows (Linux forces you to work with mimetypes, and Windows forces you to work with only extensions, so filehandoff and ProgramChooser need to be separate projects due to the change in expected input).
