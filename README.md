<h1>TCP Chatroom</h1>
<h3><em>TCP Chatroom is a C# project that allows users on the same local network to chat via the console.</em></h3>
<h2>Features Include:</h2>
<ul>
<li>Multiple users can chat over the local network.</li>
<li>Notifications are sent to all users when a person joins or leaves the chat.</li>
<li>All messages and notifications are logged to a text file.</li>
</ul>
<h2>Development Notes:</h2>
<ul>
<li>All users are stored in a Dictionary.</li>
<li>All messages are stored in a Queue.</li>
<li>SOLID deisgn principles were used in the following ways:</li>
<ul>
<li>The dependency injection design pattern was used for logging. This creates low coupling and high cohesion. This satisfies the "D" in the SOLID principles &mdash; The Dependency Inversion Principle.</li>
<li>The Single Responsibility ("S" in SOLID) is satisfied in the CheckIfConnected method in the User class. This method does one thing and one thing well.</li>
</ul>
</ul>