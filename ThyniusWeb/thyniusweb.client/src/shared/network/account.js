import useFetch from "../hooks/useFetch";

export function getInstallerUrl(){
    return useFetch("/installer");
}

export async function EditProfile(name, image){
    let formData = new FormData();
    formData.append("name", name);

    if(!image.includes("thynius")){ // to check if user changed profile picture or not ** WILL BUG IF IMAGE NAME CONTAINS THYNIUS BUT WILL BE RARE**
        let blob = await fetch(image).then(r => r.blob());
        formData.append("profilePicture", blob);
    }

    return fetch("/account", {
        method: "PUT",
        body: formData
    });
}