# Notes

## Assumptions

- **Palindrome:** non-English letters will count as letters, but `é` is not treated as `e`.
- **Permissions:** `null` arguments to `IsAllowed` isnt checked. Duplicate permissions in the list are okay.
- **Event bus:**
  - A handler only receives the exact event type it subscribed to.
  - Subscribing the same handler twice means it runs twice.
  - If a handler throws, the exception reaches the caller of `Publish` and later handlers don't run.
  - The bus is single-threaded.

## Trade-offs

- **Palindrome:** Two pointers does not create a parsed 'clean' string, if needed for other functions in the future, due to it's two pointer design.
- **Permissions:** `Contains` on a record is one-line O(n). A `HashSet` would be faster for large lists, but the simple version is clean and elegant.
- **Event bus:** Handlers are stored as `Delegate` and cast back to `Action<TEvent>`, this makes it safe because they're only stored under their own type. Publishing iterates over a copy so a handler can subscribe during a publish.

## Not done / with more time

- Permissions: Allow more flexibility with rules instead of a strict check.
- Event bus: could add unsubscribe, null checks, and more handler exceptions depending on the needs.

## Time taken

- around 95 mins