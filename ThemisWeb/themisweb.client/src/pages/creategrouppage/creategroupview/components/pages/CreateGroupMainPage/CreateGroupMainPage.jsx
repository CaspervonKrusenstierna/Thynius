import React, { useContext, useEffect, useState } from 'react'
import "./CreateGroupMainPage.css"
import { createGroupContext } from '../../../CreateGroupView'
import {Input} from "@nextui-org/input";

const CreateGroupMainPage = (props) => {
  const createGroupInfo = useContext(createGroupContext);
  const [image, setImage] = useState(createGroupInfo.current.groupImg)

  const updateName = (Name) => {
    createGroupInfo.current.groupName = Name;
  }

  const updateImg = (Img) => {
    createGroupInfo.current.groupImg = Img;
    setImage(Img);
  }

  return (
    <div className='CreateGroupMainPage'>
          <Input type="name" label="Gruppnamn" radius="sm" className="w-60 h-15" onChange={(e) => {updateName(e.target.value)}}defaultValue={createGroupInfo.current.groupName}></Input>
        <div className="ml-[0.75rem]">
          <p className="text-xl">Gruppbild</p>
          <input type="file"  accept=".jpg,.img" className="mt-3" onChange={(e) => {updateImg(URL.createObjectURL(e.target.files[0]))}}></input>
          {image ? <img src={image} className='w-[230px] h-[230px] sm:w-[275px] sm:h-[275px]'></img> : <></>}
        </div>

    </div>
  )
}

export default CreateGroupMainPage