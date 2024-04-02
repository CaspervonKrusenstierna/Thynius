import React from 'react'

import {
    Menubar,
    MenubarContent,
    MenubarItem,
    MenubarMenu,
    MenubarSeparator,
    MenubarTrigger,
  } from "@/components/ui/menubar"
  import {
    AlertDialog,
    AlertDialogAction,
    AlertDialogCancel,
    AlertDialogContent,
    AlertDialogDescription,
    AlertDialogFooter,
    AlertDialogHeader,
    AlertDialogTitle,
    AlertDialogTrigger,
  } from "@/components/ui/alert-dialog"

import { useNavigate } from 'react-router-dom'

  const GroupOptions = (props) => {
    const navigate = useNavigate()
  return (
    <div className='absolute ml-[250px] md:ml-[210px] '>
        <Menubar>
            <MenubarMenu>
                <MenubarTrigger>Ändra</MenubarTrigger>
                <MenubarContent>
                <MenubarItem onSelect={() => {navigate("/editgroup/general" + props.groupId)}}>Ändra Gruppinställningar</MenubarItem>
                <MenubarSeparator />

                <AlertDialog>
                    <AlertDialogTrigger asChild><MenubarItem onSelect={(e) => {e.preventDefault()}}>Ta Bort</MenubarItem></AlertDialogTrigger>
                    <AlertDialogContent>
                        <AlertDialogHeader>
                        <AlertDialogTitle>Är du helt säker?</AlertDialogTitle>
                        <AlertDialogDescription>
                            Du kan inte ångra dig efter.
                        </AlertDialogDescription>
                        </AlertDialogHeader>
                        <AlertDialogFooter>
                        <AlertDialogCancel className="outline-none border-none">Ångra</AlertDialogCancel>
                        <AlertDialogAction>Ta Bort</AlertDialogAction>
                        </AlertDialogFooter>
                    </AlertDialogContent>
                </AlertDialog>


                </MenubarContent>
            </MenubarMenu>
        </Menubar>
    </div>
  )
}

export default GroupOptions