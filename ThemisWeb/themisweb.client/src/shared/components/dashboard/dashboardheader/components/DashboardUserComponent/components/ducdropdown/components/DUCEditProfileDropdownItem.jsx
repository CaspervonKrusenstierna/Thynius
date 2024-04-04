import React, { useState } from 'react'
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

const DUCEditProfileDropdownItem = () => {
  const [image, setImage] = useState("https://github.com/shadcn.png")
  return (
        <Dialog>
            <DialogTrigger asChild>
                <button className='DUCDropdownItem'>Ändra Profil</button>
            </DialogTrigger>
            <DialogContent className="sm:max-w-[500px]">
                <DialogHeader>
                    <DialogTitle>Ändra Profil</DialogTitle>
                </DialogHeader>
                <div className="flex-column">
                    <div className='flex flex-row justify-start w-full h-20 mt-[5px]'>
                        <Avatar className="h-20 w-20"><AvatarImage src={image} /><input type="file" onChange={(e) => {setImage(URL.createObjectURL(e.target.files[0]))}}  accept=".jpg,.img" className='absolute w-20 h-20 outline-none z-99 cursor-pointer pt-20'></input></Avatar>
                    </div>
                    <Input type="Namn" variant="faded" label="Namn" className="w-full mt-3"></Input>
                    <Input type="name" variant="faded" label="Lösenord" className="w-full mt-4"></Input>
                </div>
                <DialogFooter>
                    <Button type="submit" size="submit">Spara</Button>
                </DialogFooter>
            </DialogContent>
    </Dialog>
  )
}

export default DUCEditProfileDropdownItem