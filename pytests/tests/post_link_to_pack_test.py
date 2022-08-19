import requests

from pytests.src.main_classes.response import Response
from pytests.src.config.configuration import SERVICE_URL
from pytests.src.schemas.post_link_to_pack_schema import POST_PACK_SCHEMA


def test_post_link_to_pack():
    r = requests.post(
        url=SERVICE_URL + 'api/v1/link/pack/?link=453', verify=False)
    response = Response(r)
    response.assert_status_code(200).validate(POST_PACK_SCHEMA)
