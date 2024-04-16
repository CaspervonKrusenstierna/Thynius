import React, { useState } from 'react'
import "./ManageGroupMainPage.css"
import {Input} from "@nextui-org/input";

const ManageGroupMainPage = (props) => {
  const [image, setImage] = useState(props.groupInfo.current.groupImg)

  const updateName = (Name) => {
    props.groupInfo.current.groupName = Name;
  }

  const updateImg = (Img) => {
    props.groupInfo.current.groupImg = URL.createObjectURL(Img);
    setImage(URL.createObjectURL(Img));
  }

  return (
    <div className='ManageGroupMainPage'>
      {props.groupInfo.current.nameError ? 
       <Input color='danger' isRequired type="name" label="Gruppnamn" radius="sm" className="w-60 h-15" onChange={(e) => {updateName(e.target.value)}}defaultValue={props.groupInfo.current.groupName}></Input> 
       :
       <Input type="name" label="Gruppnamn" radius="sm" className="w-60 h-15" onChange={(e) => {updateName(e.target.value)}}defaultValue={props.groupInfo.current.groupName}></Input>
       }
        <div className="ml-[0.75rem]">
          <p className={props.groupInfo.current.pictureError ? "text-red-600 text-xl" : "text-xl"}>Gruppbild</p>
          <input type="file"  accept=".jpg,.img" className="mt-" onChange={(e) => {updateImg(e.target.files[0])}}></input>
          {image ? <img src={image} className='w-[230px] h-[230px] sm:w-[275px] sm:h-[275px]'></img> : <></>}
        </div>

    </div>
  )
}

export default ManageGroupMainPage