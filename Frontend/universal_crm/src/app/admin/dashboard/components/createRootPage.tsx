'use client';
import { useRouter } from "next/navigation";
import { useState } from 'react';
import { Button, Container, Form } from 'react-bootstrap';
import { pageService } from '../services/pageService';
import { RootPage } from "../types/RootPage";

export const CreateRootPageForm = () => {
    const router = useRouter();

    const [formData, setFormData] = useState<RootPage>({
        Title: '',
        Path: '',
        OrdinalNum: 0,
        Content: '',
        MetaTitle: '',
        MetaDescription: '',
        MetaKeywords: '',
        IsActive: true
    });
    const [error, setError] = useState<string | null>(null);
    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value, type } = e.target;

        setFormData(prev => ({
            ...prev,
            [name]: type === 'checkbox' ? (e.target as HTMLInputElement).checked : value
        }));
    };
    async function handleSubmit(e: React.FormEvent) {
        e.preventDefault();
        setError(null);

        try {
            const response = await pageService.createRoot(formData);
            console.log(response.status)
            if (response.status == 201) {
                router.push("/admin/dashboard/get_all_pages");
            }
        }
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        catch (error: any) {
            setError(error.message);
        }
    }

    return (
        <Container className="mt-4">
            <Form onSubmit={handleSubmit}>
                <Form.Group className="mb-3">
                    <Form.Label>Title</Form.Label>
                    <Form.Control
                        type="text"
                        name="Title"
                        value={formData.Title}
                        required
                        onChange={handleChange}
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Path</Form.Label>
                    <Form.Control
                        type="text"
                        name="Path"
                        value={formData.Path}
                        required
                        onChange={handleChange}
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Порядковый номер</Form.Label>
                    <Form.Control
                        type="number"
                        name="OrdinalNum"
                        value={formData.OrdinalNum}
                        onChange={handleChange}
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Content</Form.Label>
                    <Form.Control
                        as="textarea"
                        rows={5}
                        name="Content"
                        value={formData.Content}
                        onChange={handleChange}
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Meta Title</Form.Label>
                    <Form.Control
                        type="text"
                        name="MetaTitle"
                        value={formData.MetaTitle}
                        onChange={handleChange}
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Meta Description</Form.Label>
                    <Form.Control
                        as="textarea"
                        rows={3}
                        name="MetaDescription"
                        value={formData.MetaDescription}
                        onChange={handleChange}
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Meta Keywords</Form.Label>
                    <Form.Control
                        type="text"
                        name="MetaKeywords"
                        value={formData.MetaKeywords}
                        onChange={handleChange}
                    />
                </Form.Group>

                <Form.Group className="mb-3" controlId="isActiveCheck">
                    <Form.Check
                        type="checkbox"
                        label="Активна?"
                        name="IsActive"
                        checked={formData.IsActive}
                        onChange={handleChange}
                    />
                </Form.Group>

                <Button type="submit" variant="primary">
                    Создать
                </Button>
                {error && <p style={{ color: "red" }}>{error}</p>}
            </Form>
        </Container>
    );
}