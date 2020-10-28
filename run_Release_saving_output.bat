cd bin
if NOT ["%errorlevel%"]==["0"] pause
cd Release
if NOT ["%errorlevel%"]==["0"] pause
ProgChooser.exe "C:\Users\Jatlivecom\GitHub\DeepFileFind-cs\DeepFileFind_Windows.sln" 1>out.txt 2>err.txt
if NOT ["%errorlevel%"]==["0"] pause