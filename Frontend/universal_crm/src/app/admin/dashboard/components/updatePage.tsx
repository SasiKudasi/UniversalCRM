'use client'
import { api } from "@/lib/api";
import { useRouter } from "next/navigation";
import { useEffect, useState } from "react";
import { Button, Container, Form } from "react-bootstrap";
import { pageService } from '../services/pageService';
import { RootPage } from "../types/RootPage";

interface UpdatePageFormProps {
    pageId: string;
}

export const UpdatePageForm = ({ pageId }: UpdatePageFormProps) => {
    const router = useRouter();
    const [formData, setFormData] = useState<RootPage | null>(null);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        if (!pageId) return;

        api.get(`/admin/${pageId}`, {
            headers: { Accept: "application/json" },
            withCredentials: true,
        })
            .then((res) => setFormData(res.data))
            .catch((err) => {
                console.error(err);
                setError("Не удалось загрузить страницу");
            });
    }, [pageId]);

    const handleChange = (
        e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
    ) => {
        const { name, value, type, checked } = e.target as HTMLInputElement;

        if (!formData) return;

        setFormData({
            ...formData,
            [name]: type === "checkbox" ? checked : value,
        });
    };

    async function handleSubmit(e: React.FormEvent) {
        e.preventDefault();
        setError(null);

        try {
            if (!formData) {
                setError("Данные страницы не загружены");
                return;
            }
            const response = await pageService.update(pageId, formData);
            console.log(response.status)
            if (response.status == 200) {
                router.push("/admin/dashboard/get_all_pages");
            }
        }
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        catch (error: any) {
            setError(error.message);
        }
    }

    if (!formData) return <p>Загрузка...</p>;

    return (
      
            <Form onSubmit={handleSubmit}>
                <Form.Group className="mb-3">
                    <Form.Label>Title</Form.Label>
                    <Form.Control
                        type="text"
                        name="title"
                        value={formData.title}
                        required
                        onChange={handleChange}
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Path</Form.Label>
                    <Form.Control
                        type="text"
                        name="path"
                        value={formData.path}
                        required
                        onChange={handleChange}
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Порядковый номер</Form.Label>
                    <Form.Control
                        type="number"
                        name="ordinalNuber"
                        value={formData.ordinalNuber ?? ""}
                        onChange={handleChange}
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Content</Form.Label>
                    <Form.Control
                        as="textarea"
                        rows={5}
                        name="content"
                        value={formData.content}
                        onChange={handleChange}
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Meta Title</Form.Label>
                    <Form.Control
                        type="text"
                        name="metaTitle"
                        value={formData.metaTitle}
                        onChange={handleChange}
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Meta Description</Form.Label>
                    <Form.Control
                        as="textarea"
                        rows={3}
                        name="metaDescription"
                        value={formData.metaDescription}
                        onChange={handleChange}
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Meta Keywords</Form.Label>
                    <Form.Control
                        type="text"
                        name="metaKeywords"
                        value={formData.metaKeywords || ""}
                        onChange={handleChange}
                    />
                </Form.Group>

                <Form.Group className="mb-3" controlId="isActiveCheck">
                    <Form.Check
                        type="checkbox"
                        label="Активна?"
                        name="isActive"
                        checked={formData.isActive}
                        onChange={handleChange}
                    />
                </Form.Group>
                <Button type="submit" variant="primary">
                    Обновить
                </Button>
                {error && <p style={{ color: "red" }}>{error}</p>}
            </Form>
      
    )
}