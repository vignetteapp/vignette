# Contributing Guidelines

Thank you for contributing to Vignette! The following is a set of guidlelines for contributing to Vignette and its libraries. We aim to provide a healthy environment for everyone involved and we have noted important things to keep in mind.

Do keep in mind that these are not "official rules" but in doing so can help everyone deal with things in an efficient manner.

## Submitting an Issue

- **Search for issues first before submitting a new issue**

To keep the issue tracker clean, we close issues that are similar to other existing issues.

- **Provide as much information as possible**

This is to help other contributors and maintainers understand what you are experiencing. Such information can be by providing your system specifications, logs, reproduction steps, or a video or picture of the bug.

- **Make issues simple and specific**

This to help contributors and other maintainers immediately get a grasp of the issue you are experiencing. Please avoid making long submissions and keep it direct to the point. If you are experiencing multiple issues, you are required to make multiple issue submissions for each one.

- **Please use discussions for feature requests**

This is another effort of keeping the issue tracker clean. All feature requests are closed and are converted into discussions. Do not fret as we read feature requests whenever possible.

- **Refrain from off-topic discussions**

This includes "+1" comments and asking questions if the issue is resolved. This is to keep the issue topic clear of unnecessary noise. You can make use of reactions instead to express your opinion.

## Submitting a Pull Request

We welcome pull requests from contributors outside the organization. You can head over to the [issues page](https://github.com/ppy/osu/issues) of this repository to get started.

Here are some key things to note before submitting a pull request:

- **Make sure you are familiar with git and the Feature Branch Workflow**

[git](https://git-scm.com/) is a version control system that can be confusing at first if you aren't familiar with version control. Basically, projects using git have a specific workflow for submitting code changes, which is called the pull request workflow.

The feature branch workflow allows specific features to be developed in their own branches and later can be merged to the main branch after conflicts and reviews have been resolved. You can read [this article](https://www.atlassian.com/git/tutorials/comparing-workflows/feature-branch-workflow) on how this workflow works in detail.

- **For new features, please create a new discussion proposing the new feature first.**

This is let the core contributors and maintainers of the project have an idea on what you have in mind and have a clear outline of what that feature entails and how it will be implemented.

To get started, head over to the [discussions page](https://github.com/vignetteapp/vignette/discussions) of this repository.

- **Refrain from using the GitHub web interface**

GitHub provides an option to edit code or replace files through the web interface. However it is highly discouraged to be used in most scenarios as there may be issues regarding whitespace or file encoding changes that may happen which will make it difficult for reviewers.

- **Add tests whenever possible**

Automated tests help the codebase be more maintainable and organised.

- **Run tests before opening a pull request**

While it is available through GitHub Actions, its best not to rely on it as there are other builds that can be queued at any time. Only make a pull request when you are sure that all tests pass.

- **Run code style analysis before opening a pull request**

While as a part of GitHub Actions, as stated before, it is best not to rely on it with the same reasons stated above.
