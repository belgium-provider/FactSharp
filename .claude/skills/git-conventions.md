# Skill: Git Conventions

> This skill defines all Git commit standards for this project.
> Apply these rules for every commit — no exceptions.

---

## Commit Message Format

```
<TYPE><emoji>(<scope>): <short description>

[optional body]

[optional footer]
```

### Rules
- Subject line: max 72 characters
- Use imperative mood: "add feature" not "added feature"
- No period at the end of the subject line
- Body: explain WHY, not WHAT (the diff already shows what changed)
- Always reference a ticket in the footer if one exists

---

## Commit Types & Emojis

| Type | Emoji | When to use | Example |
|---|---|---|---|
| `FEAT` | 🚀 | Brand new feature from scratch | `FEAT🚀(auth): add user registration flow` |
| `NEW` | 🚀 | New sub-feature or new element within an existing feature | `NEW🚀(users): add avatar upload support` |
| `UPDATE` | 🚀 | Enhancement or addition to an existing feature | `UPDATE🚀(users): add pagination to user list` |
| `FIX` | 🔧 | Bug fix | `FIX🔧(auth): correct display bug on login page` |
| `PERF` | _(none)_ | Performance improvement | `PERF(db): optimize user query with index` |
| `REVERT` | _(none)_ | Revert a previous commit | `REVERT(auth): revert refresh token changes` |
| `REFACTOR` | ♻️ | Code restructuring without behavior change | `REFACTOR♻️(users): optimize data sorting logic` |
| `DOCS` | 📚 | Documentation only | `DOCS📚(api): update authentication endpoints doc` |
| `STYLE` | 🎨 | Formatting, whitespace, no logic change | `STYLE🎨(users): reformat UserService for readability` |
| `TEST` | ✅ | Adding or updating tests | `TEST✅(users): add unit tests for UserService` |
| `CHORE` | 🧹 | Dependencies, tooling, build config | `CHORE🧹(deps): update NuGet packages` |
| `MERGE` | 🔄 | Branch merge commit | `MERGE🔄(develop): merge feature/new-dashboard into develop` |
| `STRUCT` | 📁 | File/folder structure reorganization | `STRUCT📁(api): reorganize controllers by domain` |

---

## Choosing the Right Type

When in doubt between similar types, use this decision tree:

```
Is it a bug fix?
  └─ YES → FIX🔧

Is it related to performance only?
  └─ YES → PERF

Is it a completely new feature that didn't exist before?
  └─ YES → FEAT🚀

Is it a new element INSIDE an existing feature?
  └─ YES → NEW🚀

Is it an improvement or addition to something that already exists?
  └─ YES → UPDATE🚀

Is it moving/renaming files or folders only?
  └─ YES → STRUCT📁

Is it code cleanup with no behavior change?
  └─ YES → REFACTOR♻️

Is it formatting only (spaces, indentation)?
  └─ YES → STYLE🎨

Is it tests only?
  └─ YES → TEST✅

Is it docs only?
  └─ YES → DOCS📚

Is it tooling, deps, CI config?
  └─ YES → CHORE🧹

Is it a merge commit?
  └─ YES → MERGE🔄

Is it undoing a previous commit?
  └─ YES → REVERT
```

---

## Scopes

Scopes define which area of the project is affected.
Always use lowercase.

| Scope | Area |
|---|---|
| `auth` | Authentication & authorization |
| `users` | User management |
| `api` | General API layer |
| `db` | Database, migrations, EF Core |
| `ui` | Frontend components |
| `infra` | Docker, deployment, infrastructure |
| `deps` | Dependencies and packages |
| `ci` | CI/CD pipelines |
| `config` | Configuration files |

---

## ✅ Good Examples

```
FEAT🚀(auth): add user registration with email verification

Implements full registration flow including email confirmation.
Token expires after 24h and is single-use.

Closes #42
```

```
FIX🔧(auth): correct display bug on login page

Error message was not showing when credentials were invalid
due to missing null check on the response object.

Fixes #87
```

```
UPDATE🚀(users): add pagination to user list endpoint

Adds limit/offset query parameters to GET /users.
Default page size is 20, max is 100.

Refs #103
```

```
NEW🚀(users): add avatar upload support

Introduces POST /users/{id}/avatar endpoint.
Images are stored in Azure Blob Storage and served via CDN.

Closes #55
```

```
REFACTOR♻️(users): optimize data sorting logic

Replaces in-memory LINQ sort with server-side ORDER BY
to reduce memory usage on large datasets.
```

```
MERGE🔄(develop): merge feature/user-dashboard into develop
```

```
STRUCT📁(api): reorganize controllers by domain

Moves all user-related controllers into /Controllers/Users/
and all auth-related into /Controllers/Auth/.
No logic changed.
```

---

## ❌ Bad Examples

```
fix bug                          # No type, no scope, too vague
FEAT: added stuff                # Past tense, no scope, no description
🚀 new feature                   # Emoji without type
feat(auth): Add JWT support.     # Lowercase type, period at end
WIP                              # Never commit WIP to shared branches
REFACTOR: changed some things    # Too vague, no scope
```

---

## Branch Naming

### Format
```
<type>/[ticket-id]-short-description
```

### Rules
- Lowercase only, hyphens to separate words
- Always include ticket ID if one exists
- Keep it short and descriptive (max 50 chars after prefix)

### ✅ Good Examples
```
feature/42-user-registration
fix/87-login-display-bug
refactor/users-sorting-logic
chore/update-nuget-packages
struct/reorganize-controllers
```

### ❌ Bad Examples
```
my-branch
Feature/AddUserEndpoint        # Uppercase
fix_the_login                  # Underscores
johns-work                     # Never use personal names
```

---

## What Claude Should Do With This Skill

When asked to **generate a commit message**:
1. Analyze the changes to identify the correct TYPE from the table above
2. Use the decision tree if the type is ambiguous
3. Include the emoji exactly as shown in the table
4. Identify the scope from the scopes table (lowercase)
5. Write a concise subject line in imperative mood, max 72 chars
6. Add a body if the change is non-obvious (explain WHY)
7. Add a footer with ticket reference if a ticket ID is provided
8. Output the full commit message ready to copy-paste

When asked to **name a branch**:
1. Identify the type (feature, fix, refactor, struct...)
2. Include the ticket ID if provided
3. Write a short kebab-case description
4. Output the full branch name ready to copy-paste

When asked to **review a commit message**:
1. Check type is valid and emoji matches
2. Check scope is present and lowercase
3. Check subject line is imperative mood and under 72 chars
4. Check no period at end
5. Flag any issues clearly with the corrected version