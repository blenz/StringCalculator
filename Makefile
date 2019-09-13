PROJECT_DIR= StringCalculator
DOCKER_IMAGE_NAME= string_calculator

alt-delimiter ?=
deny-negatives ?=
upperbound ?=
run:
	@dotnet run \
		$(alt-delimiter) \
		$(deny-negatives) \
		$(upperbound) \
		--project $(PROJECT_DIR)

test:
	@dotnet test

docker-build:
	@docker build -t $(DOCKER_IMAGE_NAME) .

docker-run:
	@docker run -it $(DOCKER_IMAGE_NAME) run \
			$(alt-delimiter) \
			$(deny-negatives) \
			$(upperbound) \
			--project $(PROJECT_DIR)

docker-test:
	@docker run $(DOCKER_IMAGE_NAME) test