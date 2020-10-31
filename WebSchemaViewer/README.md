To build this you need to run the tyescript build from the menu
Then run npm run webpack -d from the root directory.
Then run live-server from the root directory.

To deploy this in database forge you need to combine the index.html with the build which is in application\Scripts\build\EditorApp.min.js.

Make sure the line var databaseJson = ''; is set in the js incase you hardcoded it while working on the view. We replace this string with json in database forge.

Make sure the line // For example <script data-search-pseudo-elements src="..."></script>
is removed. Chrome picks up the </script> in the comment as a closing script tag even though it's in a comment which breaks everything. 