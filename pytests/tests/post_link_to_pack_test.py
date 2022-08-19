import requests

from pytests.src.config.configuration import SERVICE_URL
from pytests.src.enums.global_enums import GlobalErrorMessages


def test_post_link_to_pack():
    response = requests.post(
        url=SERVICE_URL + 'api/v1/link/pack/?link=453', verify=False)
    assert response.status_code == 200, GlobalErrorMessages.WRONG_STATUS_CODE.value
