import React, { useContext, useRef, useState } from 'react'
import { Button } from "@/components/ui/button"
import {
  Dialog,
  DialogContent,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog"
import { Avatar, AvatarImage } from '@/components/ui/avatar';
import {Input} from "@nextui-org/input";
import { sessionInfoContext } from '../../../../../../../../../App';
import { useTranslation } from 'react-i18next';
import { EditProfile } from '../../../../../../../../network/account';

const DUCEditProfileDropdownItem = () => {
  const [t, un18] = useTranslation();
  const sessionInfo = useContext(sessionInfoContext).sessionInfo;
  const [image, setImage] = useState(sessionInfo.ProfilePictureUrl);
  const name = useRef(sessionInfo.Username);
  const [error, setError] = useState(null);

  async function onSubmit(){
    if(name.current.length < 6){
        setError(t("Your name needs to contain atleast 9 characters."));
    }
    if((name.current.match(/ /g) || []).length < 1){
        setError(t("Please include both your surname and last name"))
    }
    await EditProfile(name.current, image);
    location.reload();
  }
  return (
        <Dialog>
            <DialogTrigger asChild>
                <button className='DUCDropdownItem'>{t("Edit Profile")}</button>
            </DialogTrigger>
            <DialogContent className="sm:max-w-[500px]">
                <DialogHeader>
                    <DialogTitle>{t("Edit Profile")}</DialogTitle>
                </DialogHeader>
                <div className="flex-column">
                    <div className='flex flex-row justify-start w-full h-20 mt-[5px]'>
                        <Avatar className="h-20 w-20"><AvatarImage src={image} /><input type="file" onChange={(e) => {setImage(URL.createObjectURL(e.target.files[0]))}}  accept=".jpg,.img" className='absolute w-20 h-20 outline-none z-99 cursor-pointer pt-20'></input></Avatar>
                    </div>
                    {error ? 
                    <Input isInvalid={true} errorMessage={error} placeholder={name.current} onChange={s => {name.current = s.target.value}} type="Namn" variant="faded" label="Namn" className="w-full mt-3"></Input>
                    :
                    <Input placeholder={name.current} onChange={s => {name.current = s.target.value}} type="Namn" variant="faded" label="Namn" className="w-full mt-3"></Input>
                    }
                    
                </div>
                <DialogFooter>
                    <Button onClick={onSubmit} type="submit" size="submit">{t("Save")}</Button>
                </DialogFooter>
            </DialogContent>
    </Dialog>
  )
}

export default DUCEditProfileDropdownItem