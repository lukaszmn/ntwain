# Scanning app

This application, based on [soukoku/ntwain](https://github.com/soukoku/ntwain/tree/v3), allows bulk scanning of documents and saving them as images.

## Using the new twaindsm.dll
By default the application will use the newer [Data Source Manager](http://sourceforge.net/projects/twain-dsm/files/TWAIN%20DSM%202%20Win/) (twaindsm.dll) if available. Put the DLL along the application's executable file.

Some older drivers will not work with DSM - disable this mode in the application.
