This is a small exam prep web application for multiple choice questions written in C# and ASP.NET. 

Open it up in Visual Studio 2012 Express for Web, edit App_Data/questions.xml to add your questions. Then run it. 

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

Date: 16 February 2013

License: GNU GPL version 3

// This program is free software: you can redistribute it and/or modify it under
// the terms of the GNU General Public License as published by the Free Software
// Foundation, either version 3 of the License, or (at your option) any later
// version.
//
// This program is distributed in the hope that it will be useful, but WITHOUT
// ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
// FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more
// details.
//
// You should have received a copy of the GNU General Public License along with
// this program.  If not, see <http://www.gnu.org/licenses/>.