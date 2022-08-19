from enum import Enum
from xml.dom import WRONG_DOCUMENT_ERR

class GlobalErrorMessages(Enum):
    WRONG_STATUS_CODE = "Received code status is not expected"