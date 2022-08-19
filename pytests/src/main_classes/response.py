from jsonschema import validate

from pytests.src.enums.global_enums import GlobalErrorMessages


class Response:
    def __init__(self, response):
        self.response = response
        self.response_data = response.json()
        self.response_status_code = response.status_code

    def validate(self, schema):
        validate(self.response_data, schema)

    def assert_status_code(self, status_code):
        if isinstance(status_code, list):
            assert self.response_status_code in status_code, GlobalErrorMessages.WRONG_STATUS_CODE.value
        else:
            assert self.response_status_code == status_code, GlobalErrorMessages.WRONG_STATUS_CODE.value
        return self