import React, { useState } from 'react'
import "./ManageGroupMainPage.css"
import {Input} from "@nextui-org/input";
import { useTranslation } from 'react-i18next';
import LoadingImage from '../loadingimage/LoadingImage';

const ManageGroupMainPage = (props) => {
  const [image, setImage] = useState(props.groupInfo.current.groupImg)
  const [t, i18n] = useTranslation();

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
       <Input isInvalid={true} errorMessage={props.groupInfo.current.nameError} label={t("Group Name")} radius="sm" className="w-60 h-15" onChange={(e) => {updateName(e.target.value)}}defaultValue={props.groupInfo.current.groupName}></Input> 
       :
       <Input type="name" label={t("Group Name")} radius="sm" className="w-60 h-15" onChange={(e) => {updateName(e.target.value)}}defaultValue={props.groupInfo.current.groupName}></Input>
       }
        <div className="ml-[0.75rem]">
          <p className={props.groupInfo.current.pictureError ? "text-red-600 text-xl" : "text-xl"}>{t("Group Image")}</p>
          <input type="file"  accept=".jpg,.img" className="mt-[5px]" onChange={(e) => {updateImg(e.target.files[0])}}></input>
          {image ? <LoadingImage img={image} className='mt-[10px] w-[230px] h-[230px] sm:w-[275px] sm:h-[275px]'></LoadingImage> : <div className='mt-[10px] w-[230px] h-[230px] sm:w-[275px] sm:h-[275px]'></div>}
        </div>

    </div>
  )
}

export default ManageGroupMainPage