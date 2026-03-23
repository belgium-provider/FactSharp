# Release Guide — FactSharp

Guide pour publier une nouvelle version du package NuGet `FactSharp`.

---

## Prérequis

Avant toute release, vérifier que le secret GitHub est bien configuré :

- **Repo** → `Settings` → `Secrets and variables` → `Actions`
- Secret requis : `NUGET_PACKAGE_TOKEN` (API key NuGet.org avec droits push sur `FactSharp`)

---

## Process de release

### 1. Mettre à jour la version

Dans [FactSharp/FactSharp.csproj](../FactSharp/FactSharp.csproj), modifier la balise `<Version>` :

```xml
<Version>1.2.0</Version>
```

Respecter le versioning sémantique :

| Type de changement | Exemple | Incrément |
|---|---|---|
| Correctif / bug fix | `1.1.0` → `1.1.1` | Patch |
| Nouvelle fonctionnalité rétrocompatible | `1.1.0` → `1.2.0` | Minor |
| Breaking change | `1.1.0` → `2.0.0` | Major |

---

### 2. Commit des changements

```bash
git add FactSharp/FactSharp.csproj
git commit -m "chore(release): bump version to 1.2.0"
git push origin main
```

---

### 3. Créer et pousser le tag

```bash
git tag v1.2.0
git push origin v1.2.0
```

> Le tag doit correspondre exactement à la version du `.csproj` préfixée de `v`.
> Exemple : `<Version>1.2.0</Version>` → tag `v1.2.0`

---

## Ce que fait le workflow automatiquement

Le push du tag déclenche [.github/workflows/main.yml](../.github/workflows/main.yml) qui exécute :

1. `dotnet restore` — restaure les dépendances
2. `dotnet build -c Release` — compile en mode Release
3. `dotnet pack` — génère le `.nupkg` dans `FactSharp/artifacts/`
4. `dotnet nuget push` — publie sur [NuGet.org](https://www.nuget.org)

---

## En cas d'erreur

| Problème | Cause probable | Solution |
|---|---|---|
| Workflow non déclenché | Tag ne commence pas par `v` | Utiliser `v1.2.0` et non `1.2.0` |
| `401 Unauthorized` sur NuGet | Secret `NUGET_PACKAGE_TOKEN` manquant ou expiré | Regénérer l'API key sur NuGet.org et mettre à jour le secret |
| `409 Conflict` sur NuGet | Version déjà publiée | Incrémenter la version dans le `.csproj` |
| Build échoué | Code non compilable | Vérifier en local avec `dotnet build -c Release` avant de tagger |

---

## Supprimer un tag (si erreur)

Si un tag a été poussé par erreur :

```bash
# Supprimer en local
git tag -d v1.2.0

# Supprimer sur le remote
git push origin --delete v1.2.0
```

Puis corriger et recréer le tag.
