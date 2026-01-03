#!/bin/bash

# Set workspace based on branch name
echo "##[group]Detecting Terraform workspace"
echo "Build Source Branch: $BUILD_SOURCEBRANCH"

if [[ "$BUILD_SOURCEBRANCH" == "refs/heads/main" ]]; then
  WORKSPACE="prd"
else
  WORKSPACE="dev"
fi

echo "Selecting Terraform workspace: $WORKSPACE"
terraform workspace select "$WORKSPACE" || terraform workspace new "$WORKSPACE"
echo "##[endgroup]"
