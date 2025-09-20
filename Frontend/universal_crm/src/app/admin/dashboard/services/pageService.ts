import { api } from "@/lib/api";
import { RootPage } from "../types/RootPage";
export const pageService = {
  createRoot: async (RootPage: RootPage) => {
    const formData = new FormData();

    formData.append('Title', RootPage.title);
    formData.append('Path', RootPage.path);
    formData.append('OrdinalNum', RootPage.ordinalNum.toString());
    formData.append('Content', RootPage.content);
    formData.append('MetaTitle', RootPage.metaTitle);
    formData.append('MetaDescription', RootPage.metaDescription);
    formData.append('MetaKeywords', RootPage.metaKeywords);
    formData.append('IsActive', RootPage.isActive.toString());


    const response = await api.post("/admin/root", formData, {
      headers: {
        'accept': 'text/plain',
        'Content-Type': 'multipart/form-data',
      },
    });
    return response;
  },

  createChild: async (parentId: string, RootPage: RootPage) => {
    const formData = new FormData();
    formData.append('Title', RootPage.title);
    formData.append('Path', RootPage.path);
    formData.append('OrdinalNum', RootPage.ordinalNum.toString());
    formData.append('Content', RootPage.content);
    formData.append('MetaTitle', RootPage.metaTitle);
    formData.append('MetaDescription', RootPage.metaDescription);
    formData.append('MetaKeywords', RootPage.metaKeywords);
    formData.append('IsActive', RootPage.isActive.toString());


    const response = await api.post("/admin/with_parent?parentID=" + parentId, formData, {
      headers: {
        'accept': 'text/plain',
        'Content-Type': 'multipart/form-data',
      },
    });
    return response;
  },

  update: async (id: string, RootPage: RootPage) => {
    const payload = {
      Title: RootPage.title,
      Path: RootPage.path,
      OrdinalNum: RootPage.ordinalNum,
      Content: RootPage.content,
      MetaTitle: RootPage.metaTitle,
      MetaDescription: RootPage.metaDescription,
      MetaKeywords: RootPage.metaKeywords,
      IsActive: RootPage.isActive,
    };

    const response = await api.patch("admin/" + id, payload, {
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
      },
    });
    return response;
  },

  delete: async (id: string) => {
    const response = await api.delete("/admin/" + id);
    return response;
  },


};