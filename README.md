This project aims to detect cheating, specifically by detecting texts generated by ChatGPT. It detects this by analysing how a text was written, instead of looking at the result.

How it works:
1. The background process waits for Word to open.
2. Word opens and the background process injects the dll into it.
3. Students write their texts in Word and the dll creates an inputs file to be sent to the server.
4. Word closes and the background process sends the inputs file to the server
5. The student finishes their text and submits it to an assignment created by the teacher in the web app.
6. The server recreates the text content trough looping trough the inputs file and also determines if the user has cheated or not.
7. The teacher then views the text and the result of the cheat detection algorithm. The teacher may also view how the text was written with millisecond precision.

Video visualising the process from a teacher/user perspective:

https://github.com/CaspervonKrusenstierna/Thynius/assets/136915966/56668f27-77ec-4b40-ba62-62d4218dab48


