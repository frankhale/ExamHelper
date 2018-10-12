This is a small exam prep web application for multiple choice questions written
in C# and ASP.NET.

This was born in January 2010 when I was studying for the Security+ exam.

Open it up in Visual Studio, edit App_Data/questions.xml to add your questions. 
Then run it. 

Question format:

```xml
<question>
  <text></text>
  <answers>
    <answer correct="true"></answer>
    <answer correct="false"></answer>
    <answer correct="false"></answer>
    <answer correct="false"></answer>
  </answers>
</question>
```

Frank Hale <frankhale@gmail.com>

Updated Date: 12 October 2013

License: GNU GPL version 3

http://www.gnu.org/licenses/gpl.txt