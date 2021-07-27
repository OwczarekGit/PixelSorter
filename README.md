
# PixelSorter
Simple app that will produce 16 color palette out of provided image.
See demo below.

## Usage:
`PixelSorter /path/to/image [Options]`

### Options:
- **-s, --stdout** - Output to stdout instead of creating picker window. (true, 1, false, 0) **Default false**
- **-f, --format** - Specify output format. (RGB, HEX) **Default HEX**
- **-c, --colors** - Specify number of colors to get. **Default 16**
- **-r, --raw**    - Print values in raw format, without # or rgb() (true, 1, false, 0) **Default true**

### Examples:
> Print values in RGB in fancy format and output to stdout:
> `PixelSorter file.png -f rgb -r false --stdout 1`

> Print values in HEX in raw format and display it in picker window:
> `PixelSorter file.png`

NOTE: This works because of the default values.

> Print 4 most frequent colors in HEX, in fancy format and output it to stdout:
> `PixelSorter file.png --raw 0 --stdout true -c 4`

NOTE: You can use both true/false or 1/0 for booleans, use the one you prefer.


https://user-images.githubusercontent.com/32412218/126044164-b43e8881-3380-47c8-a746-0a9be3a86430.mp4
