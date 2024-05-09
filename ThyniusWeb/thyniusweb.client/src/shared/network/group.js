import useFetch from "../hooks/useFetch";

export async function createGroup(name, members, image){
    let formData = new FormData();
    formData.append("name", name);
    if(members.length > 0){
        formData.append("users", members.map(i => i.id));
    }
    let blob = await fetch(image).then(r => r.blob());
    formData.append("image", blob);
    return fetch("/group", {
        method: "POST",
        body: formData
    });
}

export async function editGroup(id, name, members, image){    
    let formData = new FormData();
    formData.append("name", name);
    formData.append("id", id);

    if(members.length >= 1){
        formData.append("users", members.map(i => i.id));
    }
    if(!image.includes("thynius")){ // to check if user changed profile picture or not  ** WILL BUG IF IMAGE NAME CONTAINS THYNIUS BUT WILL BE RARE **
        let blob = await fetch(image).then(r => r.blob());
        formData.append("image", blob);
    }

    return fetch("/group", {
        method: "PUT",
        body: formData
    });
}

export function getGroupInfo(groupId){

}
export function getGroupMembers(groupId){
    return useFetch("/users/groupusers?groupId="+groupId, "GET");
}

export function deleteGroup(groupId){
    return useFetch("/group?groupId="+groupId, "DELETE");
}