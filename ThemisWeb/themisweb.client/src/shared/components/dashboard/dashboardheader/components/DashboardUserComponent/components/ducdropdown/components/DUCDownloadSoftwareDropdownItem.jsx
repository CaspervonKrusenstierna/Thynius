import React, { useState } from 'react'
import { Button } from "@/components/ui/button"
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog"

const DUCDownloadSoftwareDropdownItem = () => {

  return (
        <Dialog>
            <DialogTrigger asChild>
                <button className='DUCDropdownItem'>Ladda ned mjukvara</button>
            </DialogTrigger>
            <DialogContent className="sm:max-w-[500px]">
                <DialogHeader>
                    <DialogTitle>Ladda ned mjukvara</DialogTitle>
                </DialogHeader>
                <div className='h-20'></div>
                <div className='flex flex-row w-full justify-center'><Button>Ladda ned</Button></div>
            </DialogContent>
    </Dialog>
  )
}

export default DUCDownloadSoftwareDropdownItem