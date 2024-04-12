import React, {useContext, useRef} from 'react'
import "./DUCProfileButton.css"
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar"
import { sessionInfoContext } from '../../../../../../../../App'

const DUCProfileButton = (props) => {
  const SessionInfo = useContext(sessionInfoContext)?.sessionInfo;
  const UserRole = useRef("");
  switch(SessionInfo?.RoleLevel){
    case 3:
      UserRole.Current = "Administratör"; break;
    case 2:
      UserRole.Current = "Administratör"; break;
    case 1:
      UserRole.Current = "Lärare"; break;
    default:
      UserRole.Current = "Elev"
  }

  return (
      <button className='DUCProfileButton' onClick={props.onClick}>
        {props.sizeState ?        
        <>
          <Avatar>
            <AvatarImage src="https://github.com/shadcn.png" />
            <AvatarFallback>CN</AvatarFallback>
          </Avatar>
          <div className='DUCProfileButton-TextContainer'>
            <p className='DUCProfileButton-Text'>{SessionInfo?.Username}</p>
            <p className='DUCProfileButton-Text'>{UserRole.Current}</p>
          </div>
        </>
         :
         <Avatar>
          <AvatarImage src="https://github.com/shadcn.png" />
          <AvatarFallback>CN</AvatarFallback>
         </Avatar>
          }

  </button>

  )
}

export default DUCProfileButton