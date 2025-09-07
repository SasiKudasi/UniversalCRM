import { api } from "@/lib/api";
import { RootPage } from "../types/RootPage";
export const pageService = {
  createRoot: async (RootPage: RootPage) => {
    const formData = new FormData();

    formData.append('Title', RootPage.Title);
    formData.append('Path', RootPage.Path);
    formData.append('OrdinalNum', RootPage.OrdinalNum.toString());
    formData.append('Content', RootPage.Content);
    formData.append('MetaTitle', RootPage.MetaTitle);
    formData.append('MetaDescription', RootPage.MetaDescription);
    formData.append('MetaKeywords', RootPage.MetaKeywords);
    formData.append('IsActive', RootPage.IsActive.toString());


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
    formData.append('Title', RootPage.Title);
    formData.append('Path', RootPage.Path);
    formData.append('OrdinalNum', RootPage.OrdinalNum.toString());
    formData.append('Content', RootPage.Content);
    formData.append('MetaTitle', RootPage.MetaTitle);
    formData.append('MetaDescription', RootPage.MetaDescription);
    formData.append('MetaKeywords', RootPage.MetaKeywords);
    formData.append('IsActive', RootPage.IsActive.toString());


    const response = await api.post("/admin/with_parent?parentID=" + parentId, formData, {
      headers: {
        'accept': 'text/plain',
        'Content-Type': 'multipart/form-data',
      },
    });
    return response;
  },

  // Другие методы для админа
  update: async (id: string, RootPage: RootPage) => {
    const formData = new FormData();
    formData.append('Title', RootPage.Title);
    formData.append('Path', RootPage.Path);
    formData.append('OrdinalNum', RootPage.OrdinalNum.toString());
    formData.append('Content', RootPage.Content);
    formData.append('MetaTitle', RootPage.MetaTitle);
    formData.append('MetaDescription', RootPage.MetaDescription);
    formData.append('MetaKeywords', RootPage.MetaKeywords);
    formData.append('IsActive', RootPage.IsActive.toString());
  },

  delete: async (id: string) => {
    const response = await api.delete("/admin/" + id);
    return response;
  }
};