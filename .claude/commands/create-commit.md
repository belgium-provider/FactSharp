# Create a new commit

> Main instruction to create beautiful commits.

---
description: Stage all changes and create a conventional commit
argument-hint: [optional scope]
---

## Process

1. Run `git status` to list all changed and untracked files
2. Run `git diff --staged` and `git diff` to read the full diff
3. Auto-detect type and scope from the diff (or use $1 if provided)
4. Follow @.claude/skills/git-conventions.md to build the commit message
5. Stage **all** changed and untracked files with `git add -A`
6. Create the commit with the generated message

## Rules

1. NEVER push without explicit user approval
2. NEVER push or force-push to main/master
3. NEVER amend a previous commit — always create a new one
4. NEVER use `--no-verify` or skip hooks
5. If a pre-commit hook fails, fix the issue and retry with a NEW commit