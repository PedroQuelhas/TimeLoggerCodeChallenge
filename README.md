# e-conomic & sproom hiring task

During this challenge, my main focus was to try and display my backend skills. As such I refactored a lot of the provided code to follow some patterns I consider good practices.
The API supports a lot of features not included in the requirements such as pagination and filtering. It also includes sorting as required.

I personally like to develop APIs by writing an Openapi (swagger) definition first and then generating the boilerplate server code, and frontend client from that.
This makes the job of keeping client and server synced much easier (if the api version is a match in both client and server, things should work), and it helps QA create API
tests faster and more efficiently. Also, as a big plus, you get documentation of your API for "free" which you can expose as a swagger page. (which is also done is the project -> http://localhost:3001/openapi/index.html)

As you may also notice, there is only one controller for the API. This is a limitation of the Codegen lib which I circumvent by using "handlers" to segregate the different responsibilities.

I only added a couple of unit tests due to the lack of time, but I think they show a variety of cases.

Regarding the frontend, as I told you before, I'm not super savvy with Typescript/React but I implemented two simple operations, the rendering of the projects in the table, and adding
na new project to the API (this one the values are hardcoded since I did not have time to make an input form etc)

I hope what I did is enough! :)