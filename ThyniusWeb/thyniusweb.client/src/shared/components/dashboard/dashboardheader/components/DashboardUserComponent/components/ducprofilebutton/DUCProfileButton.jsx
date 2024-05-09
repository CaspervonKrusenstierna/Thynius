import React, {useContext, useRef} from 'react'
import "./DUCProfileButton.css"
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar"
import { sessionInfoContext } from '../../../../../../../../App'
import { useTranslation } from 'react-i18next'

const DUCProfileButton = (props) => {
  const [t, i18n] = useTranslation();
  const SessionInfo = useContext(sessionInfoContext)?.sessionInfo;
  const UserRole = useRef("");
  switch(SessionInfo?.RoleLevel){
    case 3:
      UserRole.Current = t("Administrator"); break;
    case 2:
      UserRole.Current = t("Administrator"); break;
    case 1:
      UserRole.Current = t("Teacher"); break;
    default:
      UserRole.Current = t("Student");
  }

  return (
      <button className='DUCProfileButton' onClick={props.onClick}>
        {props.sizeState ?        
        <>
          <Avatar>
            <AvatarImage src={SessionInfo?.ProfilePictureUrl}/>
            <AvatarFallback>CN</AvatarFallback>
          </Avatar>
          <div className={SessionInfo?.Username?.length > 24 ? 'DUCProfileButton-TextContainer leading-[16px]' :  'DUCProfileButton-TextContainer'}>
            <p className='DUCProfileButton-Text'>{SessionInfo?.Username}</p>
            <p className='DUCProfileButton-Text'>{UserRole.Current}</p>
          </div>
        </>
         :
         <Avatar>
          <AvatarImage src={SessionInfo?.ProfilePictureUrl}/>
          <AvatarFallback>CN</AvatarFallback>
         </Avatar>
          }

  </button>

  )
}

export default DUCProfileButton