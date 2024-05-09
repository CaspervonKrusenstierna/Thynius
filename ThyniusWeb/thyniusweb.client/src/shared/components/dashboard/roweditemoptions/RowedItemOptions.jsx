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

const RowedItemOptions = (props) => {
    const navigate = useNavigate()

    return (
    <div className={props.className}>
        <Menubar>
            <MenubarMenu>
                <MenubarTrigger>{props.title}</MenubarTrigger>
                <MenubarContent>
                {props.items.map(i => 
                i.type == "DialogTrigger" ?
                    <AlertDialog>
                        <AlertDialogTrigger asChild><MenubarItem onSelect={(e) => {e.preventDefault()}}>{i.text}</MenubarItem></AlertDialogTrigger>
                        <AlertDialogContent>
                            <AlertDialogHeader>
                            <AlertDialogTitle>Är du helt säker?</AlertDialogTitle>
                            <AlertDialogDescription>
                                Du kan inte ångra dig efter.
                            </AlertDialogDescription>
                            </AlertDialogHeader>
                            <AlertDialogFooter>
                            <AlertDialogCancel className="outline-none border-none">Ångra</AlertDialogCancel>
                            <AlertDialogAction onClick={i.onSelect}>{i.text}</AlertDialogAction>
                            </AlertDialogFooter>
                        </AlertDialogContent>
                    </AlertDialog>
                :
                <MenubarItem onSelect={i.onSelect}>{i.text}</MenubarItem>
                )}
                </MenubarContent>
            </MenubarMenu>
        </Menubar>
    </div>
    )
}

export default RowedItemOptions